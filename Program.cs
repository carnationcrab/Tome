using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tome.API.Data;
using Tome.API.Models;
using Tome.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Load Configuration
var configuration = builder.Configuration;

// Database
builder.Services.AddDbContext<TomeDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Identity (User Management)
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<TomeDbContext>()
.AddDefaultTokenProviders();

// JWT Authentication
var jwtKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key is missing.");
var jwtIssuer = configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("JWT Issuer is missing.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true; // Set false in dev if needed
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Enable Authorization
builder.Services.AddAuthorization();

// Register Application Services (Dependency Injection) 
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UniverseService>();
builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<CharacterTypeService>();
builder.Services.AddScoped<FieldService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<ModifierService>();
builder.Services.AddScoped<ModifierAssignmentService>();

// Enable Cross-Origin Requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Middleware Pipeline
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
