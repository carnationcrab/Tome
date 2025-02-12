using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.DTOs;
using Tome.API.Models;

namespace Tome.API.Services
{
    public class ModifierService
    {
        private readonly TomeDbContext _context;

        public ModifierService(TomeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all modifiers
        /// </summary>
        public async Task<IEnumerable<ModifierDTO>> GetAllModifiersAsync()
        {
            return await _context.Modifiers
                .Select(m => new ModifierDTO
                {
                    id = m.id,
                    name = m.name,
                    value = m.value,
                    duration = m.duration,
                    condition = m.condition
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get a modifier by ID
        /// </summary>
        public async Task<ModifierDTO?> GetModifierByIdAsync(Guid id)
        {
            var modifier = await _context.Modifiers.FindAsync(id);
            if (modifier == null) return null;

            return new ModifierDTO
            {
                id = modifier.id,
                name = modifier.name,
                value = modifier.value,
                duration = modifier.duration,
                condition = modifier.condition
            };
        }

        /// <summary>
        /// Create a new modifier
        /// </summary>
        public async Task<ModifierDTO> CreateModifierAsync(CreateModifierDTO dto)
        {
            var modifier = new Modifier
            {
                id = Guid.NewGuid(),
                name = dto.name,
                value = dto.value,
                operation = dto.operation,
                duration = dto.duration,
                condition = dto.condition
            };

            _context.Modifiers.Add(modifier);
            await _context.SaveChangesAsync();

            return new ModifierDTO
            {
                id = modifier.id,
                name = modifier.name,
                operation = modifier.operation,
                value = modifier.value,
                duration = modifier.duration,
                condition = modifier.condition
            };
        }

        /// <summary>
        /// Update an existing modifier
        /// </summary>
        public async Task<bool> UpdateModifierAsync(Guid id, UpdateModifierDTO dto)
        {
            var modifier = await _context.Modifiers.FindAsync(id);
            if (modifier == null) return false;

            modifier.name = dto.name;
            modifier.value = dto.value;
            modifier.duration = dto.duration;
            modifier.condition = dto.condition;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete a modifier
        /// </summary>
        public async Task<bool> DeleteModifierAsync(Guid id)
        {
            var modifier = await _context.Modifiers.FindAsync(id);
            if (modifier == null) return false;

            _context.Modifiers.Remove(modifier);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Assign a modifier to an entity (character, character type, item, or field)
        /// </summary>
        public async Task<bool> AssignModifierAsync(AssignModifierDTO dto)
        {
            var modifier = await _context.Modifiers.FindAsync(dto.modifierId);
            if (modifier == null) return false;

            var assignment = new ModifierAssignment
            {
                id = Guid.NewGuid(),
                modifierId = dto.modifierId,
                characterId = dto.characterId,
                characterTypeId = dto.characterTypeId,
                itemId = dto.itemId,
                fieldId = dto.fieldId
            };

            _context.ModifierAssignments.Add(assignment);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Remove an assigned modifier
        /// </summary>
        public async Task<bool> RemoveModifierAssignmentAsync(Guid assignmentId)
        {
            var assignment = await _context.ModifierAssignments.FindAsync(assignmentId);
            if (assignment == null) return false;

            _context.ModifierAssignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
