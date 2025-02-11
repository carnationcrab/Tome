using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.DTOs;
using Tome.API.Models;

namespace Tome.API.Services
{
    public class CharacterService
    {
        private readonly TomeDbContext _context;

        public CharacterService(TomeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all characters within a given universe.
        /// </summary>
        public async Task<IEnumerable<CharacterDTO>> GetCharactersByUniverseAsync(Guid universeId)
        {
            return await _context.Characters
                .Where(c => c.universeId == universeId)
                .Select(c => new CharacterDTO
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                    characterTypeId = c.characterTypeId,
                    fields = _context.CharacterFields
                        .Where(cf => cf.characterId == c.id)
                        .Select(cf => new CharacterFieldDTO
                        {
                            id = cf.fieldId,
                            name = cf.field.name,
                            value = cf.value,
                            isCustom = cf.isCustom,
                            visibility = cf.visibility
                        }).ToList()
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific character by ID, including its fields.
        /// </summary>
        public async Task<CharacterDTO?> GetCharacterByIdAsync(Guid id)
        {
            var character = await _context.Characters
                .Include(c => c.characterType)
                .FirstOrDefaultAsync(c => c.id == id);

            if (character == null) return null;

            return new CharacterDTO
            {
                id = character.id,
                name = character.name,
                description = character.description,
                characterTypeId = character.characterTypeId,
                fields = await GetCharacterFieldsAsync(character.id)
            };
        }

        /// <summary>
        /// Creates a new character, inheriting fields from CharacterType and allowing custom fields.
        /// </summary>
public async Task<CharacterDTO> CreateCharacterAsync(Guid universe_Id, CreateCharacterDTO dto)
{
    // Ensure the universe exists
    var universeExists = await _context.Universes.AnyAsync(u => u.id == universe_Id);
    if (!universeExists)
    {
        throw new Exception($"Universe with ID {universe_Id} does not exist.");
    }

    var character = new Character
    {
        id = Guid.NewGuid(),
        name = dto.name,
        description = dto.description,
        universeId = universe_Id,
        characterTypeId = dto.characterTypeId
    };

    _context.Characters.Add(character);
    await _context.SaveChangesAsync();

    // Fetch inherited fields from the CharacterType
    if (dto.characterTypeId.HasValue)
    {
        var inheritedFields = await _context.CharacterTypeFields
            .Where(ctf => ctf.characterTypeId == dto.characterTypeId)
            .Select(ctf => new CharacterField
            {
                characterId = character.id,
                fieldId = ctf.fieldId,
                value = "", // Default empty value
                isCustom = false,
                visibility = "universe"
            })
            .ToListAsync();

        _context.CharacterFields.AddRange(inheritedFields);
    }

    // Add custom fields if any
    if (dto.customFields != null)
    {
        foreach (var customField in dto.customFields)
        {
            _context.CharacterFields.Add(new CharacterField
            {
                characterId = character.id,
                fieldId = customField.fieldId,
                value = customField.value,
                isCustom = true,
                visibility = customField.visibility
            });
        }
    }

    await _context.SaveChangesAsync();

    return new CharacterDTO
    {
        id = character.id,
        name = character.name,
        description = character.description,
        characterTypeId = character.characterTypeId
    };
}

        /// <summary>
        /// Updates a character's details and custom fields.
        /// </summary>
        public async Task<bool> UpdateCharacterAsync(Guid id, UpdateCharacterDTO dto)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return false;

            character.name = dto.name;
            character.description = dto.description;

            await _context.SaveChangesAsync();

            // Update custom fields
            if (dto.customFields != null)
            {
                foreach (var updatedField in dto.customFields)
                {
                    var fieldEntry = await _context.CharacterFields
                        .FirstOrDefaultAsync(cf => cf.characterId == id && cf.fieldId == updatedField.fieldId && cf.isCustom);

                    if (fieldEntry != null)
                    {
                        fieldEntry.value = updatedField.value;
                        fieldEntry.visibility = updatedField.visibility;
                    }
                    else
                    {
                        // Add new custom field if it doesn't exist
                        _context.CharacterFields.Add(new CharacterField
                        {
                            characterId = id,
                            fieldId = updatedField.fieldId,
                            value = updatedField.value,
                            isCustom = true,
                            visibility = updatedField.visibility
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a character and removes all its associated fields.
        /// </summary>
        public async Task<bool> DeleteCharacterAsync(Guid id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return false;

            // Delete associated CharacterFields first
            var characterFields = _context.CharacterFields.Where(cf => cf.characterId == id);
            _context.CharacterFields.RemoveRange(characterFields);

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves all fields (inherited and custom) for a character.
        /// </summary>
        private async Task<List<CharacterFieldDTO>> GetCharacterFieldsAsync(Guid characterId)
        {
            return await _context.CharacterFields
                .Where(cf => cf.characterId == characterId)
                .Select(cf => new CharacterFieldDTO
                {
                    id = cf.fieldId,
                    name = cf.field.name,
                    value = cf.value,
                    isCustom = cf.isCustom,
                    visibility = cf.visibility
                })
                .ToListAsync();
        }
    }
}
