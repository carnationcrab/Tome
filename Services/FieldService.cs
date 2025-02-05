using Microsoft.EntityFrameworkCore;
using Tome.Data;
using Tome.DTOs;
using Tome.Models;

namespace Tome.Services
{
    public class FieldService
    {
        private readonly TomeDbContext _context;

        public FieldService(TomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FieldDTO>> GetFieldsByCharacterTypeIdAsync(Guid characterTypeId)
        {
            return await _context.Fields
                .Where(f => f.characterTypeId == characterTypeId)
                .Select(f => new FieldDTO
                {
                    id = f.id,
                    name = f.name,
                    type = f.type
                })
                .ToListAsync();
        }

        public async Task<FieldDTO> CreateFieldAsync(Guid characterTypeId, CreateFieldDTO dto)
        {
            var field = new Field
            {
                name = dto.name,
                type = dto.type,
                characterTypeId = characterTypeId
            };

            _context.Fields.Add(field);
            await _context.SaveChangesAsync();

            return new FieldDTO
            {
                id = field.id,
                name = field.name,
                type = field.type
            };
        }

        public async Task<bool> UpdateFieldAsync(Guid id, UpdateFieldDTO dto)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null) return false;

            field.name = dto.name;
            field.type = dto.type;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFieldAsync(Guid id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null) return false;

            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
