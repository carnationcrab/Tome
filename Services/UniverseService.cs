
 using Microsoft.EntityFrameworkCore;
 using Tome.API.Data;
 using Tome.API.DTOs;
 using Tome.API.Models;

 namespace Tome.API.Services
{
    public class UniverseService
    {
        private readonly TomeDbContext _context;

        public UniverseService(TomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UniverseDTO>> GetAllUniversesAsync()
        {
            return await _context.Universes
                .Select(u => new UniverseDTO { id = u.id, name = u.name, description = u.description })
                .ToListAsync();
        }

        public async Task<UniverseDTO> GetUniverseByIdAsync(Guid id)
        {
            var universe = await _context.Universes.FindAsync(id);
            if (universe == null) return null;

            return new UniverseDTO { id = universe.id, name = universe.name, description = universe.description };
        }

        public async Task<UniverseDTO> CreateUniverseAsync(CreateUniverseDTO dto)
        {
            var universe = new Universe { name = dto.name, description = dto.description };
            _context.Universes.Add(universe);
            await _context.SaveChangesAsync();

            return new UniverseDTO { id = universe.id, name = universe.name, description = universe.description };
        }

        public async Task<bool> UpdateUniverseAsync(Guid id, UniverseDTO dto)
        {
            var universe = await _context.Universes.FindAsync(id);
            if (universe == null) return false;

            universe.name = dto.name;
            universe.description = dto.description;
            universe.updatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUniverseAsync(Guid id)
        {
            var universe = await _context.Universes.FindAsync(id);
            if (universe == null) return false;

            _context.Universes.Remove(universe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
