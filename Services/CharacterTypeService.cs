using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.DTOs;
using Tome.API.Models;

namespace Tome.API.Services
{
    public class CharacterTypeService
    {
        private readonly TomeDbContext _context;

        public CharacterTypeService(TomeDbContext context)
        {
            _context = context;
        }

        public async Task<CharacterType?> GetCharacterTypeByIdAsync(Guid id)
        {
            return await _context.CharacterTypes
                .Include(ct => ct.fields) // Include fields if relevant
                .FirstOrDefaultAsync(ct => ct.id == id);
        }


        public async Task<List<CharacterType>> GetCharacterTypesByUniverseIdAsync(Guid universeId)
        {
            var characterTypeIds = await _context.UniverseCharacterTypes
                .Where(uct => uct.universeId == universeId)
                .Select(uct => uct.characterTypeId)
                .ToListAsync();

            return await _context.CharacterTypes
                .Where(ct => ct.visibility == "public" || characterTypeIds.Contains(ct.id))
                .ToListAsync();
        }


        public async Task<CharacterTypeDTO> CreateCharacterTypeAsync(Guid universeId, CreateCharacterTypeDTO dto)
        {
            var characterType = new CharacterType
            {
                name = dto.name,
                 //universeId = universeId,
                fields = dto.fields?.Select(f => new Field { name = f.name, type = f.type, required = f.required }).ToList()
            };

            _context.CharacterTypes.Add(characterType);
            await _context.SaveChangesAsync();

            return new CharacterTypeDTO
            {
                id = characterType.id,
                name = characterType.name,
                fields = characterType.fields.Select(f => new FieldDTO { id = f.id, name = f.name, type = f.type, required = f.required }).ToList()
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

        public async Task<bool> SetCharacterTypeVisibilityAsync(Guid id, string visibility)
        {
            var characterType = await _context.CharacterTypes.FirstOrDefaultAsync(ct => ct.id == id);
            if (characterType == null)
                return false;

            characterType.visibility = visibility;
            await _context.SaveChangesAsync();
            return true;
        }


    }
}