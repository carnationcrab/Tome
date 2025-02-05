using Microsoft.EntityFrameworkCore;
using Tome.Data;
using Tome.DTOs;
using Tome.Models;

namespace Tome.Services
{
    public class CharacterTypeService
    {
        private readonly TomeDbContext _context;

        public CharacterTypeService(TomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CharacterTypeDTO>> GetCharacterTypesByUniverseIdAsync(Guid universeId)
        {
            return await _context.CharacterTypes
                .Where(ct => ct.universeId == universeId)
                .Select(ct => new CharacterTypeDTO
                {
                    id = ct.id,
                    name = ct.name,
                    fields = ct.fields.Select(f => new FieldDTO
                    {
                        id = f.id,
                        name = f.name,
                        type = f.type
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<CharacterTypeDTO> CreateCharacterTypeAsync(Guid universeId, CreateCharacterTypeDTO dto)
        {
            var characterType = new CharacterType
            {
                name = dto.name,
                universeId = universeId,
                fields = dto.fields?.Select(f => new Field { name = f.name, type = f.type }).ToList()
            };

            _context.CharacterTypes.Add(characterType);
            await _context.SaveChangesAsync();

            return new CharacterTypeDTO
            {
                id = characterType.id,
                name = characterType.name,
                fields = characterType.fields.Select(f => new FieldDTO { id = f.id, name = f.name, type = f.type }).ToList()
            };
        }

        public async Task<bool> DeleteCharacterTypeAsync(Guid id)
        {
            var characterType = await _context.CharacterTypes.FindAsync(id);
            if (characterType == null) return false;

            _context.CharacterTypes.Remove(characterType);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}