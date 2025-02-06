using Microsoft.AspNetCore.Identity;
using Tome.API.Models;
using Tome.API.DTOs;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Tome.API.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterDTO model)
        {
            var user = new User
            {
                UserName = model.username,
                Email = model.email
            };

            var result = await _userManager.CreateAsync(user, model.password);
            return result;
        }

        public async Task<string?> LoginUserAsync(LoginDTO model)
        {
            var user = await _userManager.Users
                .Where(u => u.UserName == model.username)
                .FirstOrDefaultAsync();

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.password))
            {
                return null; // Invalid credentials
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userClaims);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Generate JWT
            return _tokenService.CreateToken(claims);
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
