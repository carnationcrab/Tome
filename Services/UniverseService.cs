using Tome.DTOs;
using Tome.Models;

namespace Tome.Services
{
    public class UniverseService
    {
        private static readonly List<Universe> _mockUniverses = new List<Universe>
        {
            new Universe { Id = Guid.NewGuid(), Name = "Mock Universe 1", Description = "A temporary universe." },
            new Universe { Id = Guid.NewGuid(), Name = "Mock Universe 2", Description = "Another placeholder." }
        };

        public async Task<IEnumerable<UniverseDTO>> GetAllUniversesAsync()
        {
            // Simulate async operation
            return await Task.FromResult(
                _mockUniverses.Select(u => new UniverseDTO { Id = u.Id, Name = u.Name, Description = u.Description })
            );
        }

        public async Task<UniverseDTO> GetUniverseByIdAsync(Guid id)
        {
            var universe = _mockUniverses.FirstOrDefault(u => u.Id == id);
            if (universe == null) return null;

            return await Task.FromResult(new UniverseDTO { Id = universe.Id, Name = universe.Name, Description = universe.Description });
        }

        public async Task<UniverseDTO> CreateUniverseAsync(CreateUniverseDTO dto)
        {
            var newUniverse = new Universe { Id = Guid.NewGuid(), Name = dto.Name, Description = dto.Description };
            _mockUniverses.Add(newUniverse);

            return await Task.FromResult(new UniverseDTO { Id = newUniverse.Id, Name = newUniverse.Name, Description = newUniverse.Description });
        }

        public async Task<bool> UpdateUniverseAsync(Guid id, UniverseDTO dto)
        {
            var universe = _mockUniverses.FirstOrDefault(u => u.Id == id);
            if (universe == null) return await Task.FromResult(false);

            universe.Name = dto.Name;
            universe.Description = dto.Description;
            universe.UpdatedAt = DateTime.UtcNow;

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteUniverseAsync(Guid id)
        {
            var universe = _mockUniverses.FirstOrDefault(u => u.Id == id);
            if (universe == null) return await Task.FromResult(false);

            _mockUniverses.Remove(universe);
            return await Task.FromResult(true);
        }
    }
}


// using Microsoft.EntityFrameworkCore;
// using Tome.Data;
// using Tome.DTOs;
// using Tome.Models;

// namespace Tome.Services
// {
//     public class UniverseService
//     {
//         private readonly TomeDbContext _context;

//         public UniverseService(TomeDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<IEnumerable<UniverseDTO>> GetAllUniversesAsync()
//         {
//             return await _context.Universes
//                 .Select(u => new UniverseDTO { Id = u.Id, Name = u.Name, Description = u.Description })
//                 .ToListAsync();
//         }

//         public async Task<UniverseDTO> GetUniverseByIdAsync(Guid id)
//         {
//             var universe = await _context.Universes.FindAsync(id);
//             if (universe == null) return null;

//             return new UniverseDTO { Id = universe.Id, Name = universe.Name, Description = universe.Description };
//         }

//         public async Task<UniverseDTO> CreateUniverseAsync(CreateUniverseDTO dto)
//         {
//             var universe = new Universe { Name = dto.Name, Description = dto.Description };
//             _context.Universes.Add(universe);
//             await _context.SaveChangesAsync();

//             return new UniverseDTO { Id = universe.Id, Name = universe.Name, Description = universe.Description };
//         }

//         public async Task<bool> UpdateUniverseAsync(Guid id, UniverseDTO dto)
//         {
//             var universe = await _context.Universes.FindAsync(id);
//             if (universe == null) return false;

//             universe.Name = dto.Name;
//             universe.Description = dto.Description;
//             universe.UpdatedAt = DateTime.UtcNow;

//             await _context.SaveChangesAsync();
//             return true;
//         }

//         public async Task<bool> DeleteUniverseAsync(Guid id)
//         {
//             var universe = await _context.Universes.FindAsync(id);
//             if (universe == null) return false;

//             _context.Universes.Remove(universe);
//             await _context.SaveChangesAsync();
//             return true;
//         }
//     }
// }
