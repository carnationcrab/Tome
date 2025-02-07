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


        public async Task<CharacterTypeDTO> CreateCharacterTypeAsync(CreateCharacterTypeDTO dto)
        {
            var characterType = new CharacterType
            {
                name = dto.name,
                visibility = dto.visibility
            };

            _context.CharacterTypes.Add(characterType);
            await _context.SaveChangesAsync();

            if (dto.fieldIds != null && dto.fieldIds.Any())
            {
                foreach (var fieldId in dto.fieldIds)
                {
                    _context.CharacterTypeFields.Add(new CharacterTypeField
                    {
                        characterTypeId = characterType.id,
                        fieldId = fieldId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return new CharacterTypeDTO { id = characterType.id, name = characterType.name };
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