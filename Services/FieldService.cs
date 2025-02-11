using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.DTOs;
using Tome.API.Models;

namespace Tome.API.Services
{
    public class FieldService
    {
        private readonly TomeDbContext _context;

        public FieldService(TomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FieldDTO>> GetAllFieldsAsync()
        {
            return await _context.Fields
                .Select(f => new FieldDTO
                {
                    id = f.id,
                    name = f.name,
                    type = f.type,
                    required = f.required
                })
                .ToListAsync();
        }

        public async Task<FieldDTO?> GetFieldByIdAsync(Guid id)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null) return null;

            return new FieldDTO
            {
                id = field.id,
                name = field.name,
                type = field.type,
                required = field.required
            };
        }

        public async Task<FieldDTO> CreateFieldAsync(CreateFieldDTO dto)
        {
            var field = new Field
            {
                name = dto.name,
                type = dto.type,
                required = dto.required
            };

            _context.Fields.Add(field);
            await _context.SaveChangesAsync();

            return new FieldDTO
            {
                id = field.id,
                name = field.name,
                type = field.type,
                required = field.required
            };
        }

        public async Task<bool> UpdateFieldAsync(Guid id, UpdateFieldDTO dto)
        {
            var field = await _context.Fields.FindAsync(id);
            if (field == null) return false;

            field.name = dto.name;
            field.type = dto.type;
            field.required = dto.required;

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

        public async Task<IEnumerable<FieldDTO>> GetFieldsByCharacterTypeIdAsync(Guid characterTypeId)
        {
            return await _context.CharacterTypeFields
                .Where(ctf => ctf.characterTypeId == characterTypeId)
                .Select(ctf => new FieldDTO
                {
                    id = ctf.field.id,
                    name = ctf.field.name,
                    type = ctf.field.type,
                    required = ctf.field.required
                })
                .ToListAsync();
        }

        public async Task<bool> AssignFieldToCharacterTypeAsync(Guid characterTypeId, Guid fieldId)
        {
            var exists = await _context.CharacterTypeFields
                .AnyAsync(ctf => ctf.characterTypeId == characterTypeId && ctf.fieldId == fieldId);
            if (exists) return false;

            _context.CharacterTypeFields.Add(new CharacterTypeField
            {
                characterTypeId = characterTypeId,
                fieldId = fieldId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CharacterFieldDTO>> GetFieldsByCharacterIdAsync(Guid characterId)
        {
            return await _context.CharacterFields
                .Where(cf => cf.characterId == characterId)
                .Select(cf => new CharacterFieldDTO
                {
                    id = cf.id,
                    name = cf.field.name,
                    value = cf.value,
                    isCustom = cf.isCustom,
                    visibility = cf.visibility
                })
                .ToListAsync();
        }

        public async Task<CharacterFieldDTO?> AddCustomFieldToCharacterAsync(Guid characterId, AddCustomFieldDTO dto)
        {
            Field field;

            if (dto.fieldId.HasValue)
            {
                // Use existing field
                field = await _context.Fields.FindAsync(dto.fieldId.Value);
                if (field == null) return null;
            }
            else
            {
                // Create a new field dynamically
                field = new Field
                {
                    name = dto.name ?? throw new ArgumentException("Field name is required when creating a new field."),
                    type = dto.type ?? throw new ArgumentException("Field type is required when creating a new field."),
                    required = false // Custom fields are not required by default
                };

                _context.Fields.Add(field);
                await _context.SaveChangesAsync();
            }

            var characterField = new CharacterField
            {
                characterId = characterId,
                fieldId = field.id,
                value = dto.value,
                isCustom = true,
                visibility = dto.visibility
            };

            _context.CharacterFields.Add(characterField);
            await _context.SaveChangesAsync();

            return new CharacterFieldDTO
            {
                id = characterField.id,
                name = field.name,
                value = characterField.value,
                isCustom = characterField.isCustom,
                visibility = characterField.visibility
            };
        }
    }
}
