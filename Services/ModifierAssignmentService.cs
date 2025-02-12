using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.DTOs;
using Tome.API.Models;

namespace Tome.API.Services
{
    public class ModifierAssignmentService
    {
        private readonly TomeDbContext _context;

        public ModifierAssignmentService(TomeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all modifier assignments
        /// </summary>
        public async Task<IEnumerable<ModifierAssignmentDTO>> GetAllModifierAssignmentsAsync()
        {
            return await _context.ModifierAssignments
                .Select(ma => new ModifierAssignmentDTO
                {
                    id = ma.id,
                    modifierId = ma.modifierId,
                    characterId = ma.characterId,
                    characterTypeId = ma.characterTypeId,
                    itemId = ma.itemId,
                    fieldId = ma.fieldId
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get a specific modifier assignment by ID
        /// </summary>
        public async Task<ModifierAssignmentDTO?> GetModifierAssignmentByIdAsync(Guid id)
        {
            var assignment = await _context.ModifierAssignments.FindAsync(id);
            if (assignment == null) return null;

            return new ModifierAssignmentDTO
            {
                id = assignment.id,
                modifierId = assignment.modifierId,
                characterId = assignment.characterId,
                characterTypeId = assignment.characterTypeId,
                itemId = assignment.itemId,
                fieldId = assignment.fieldId
            };
        }

        /// <summary>
        /// Assign a modifier to an entity (Character, CharacterType, Item, Field)
        /// </summary>
        public async Task<ModifierAssignmentDTO> AssignModifierAsync(CreateModifierAssignmentDTO dto)
        {
            var assignment = new ModifierAssignment
            {
                modifierId = dto.modifierId,
                characterId = dto.characterId,
                characterTypeId = dto.characterTypeId,
                itemId = dto.itemId,
                fieldId = dto.fieldId
            };

            _context.ModifierAssignments.Add(assignment);
            await _context.SaveChangesAsync();

            return new ModifierAssignmentDTO
            {
                id = assignment.id,
                modifierId = assignment.modifierId,
                characterId = assignment.characterId,
                characterTypeId = assignment.characterTypeId,
                itemId = assignment.itemId,
                fieldId = assignment.fieldId
            };
        }

        /// <summary>
        /// Remove a modifier assignment
        /// </summary>
        public async Task<bool> RemoveModifierAssignmentAsync(Guid id)
        {
            var assignment = await _context.ModifierAssignments.FindAsync(id);
            if (assignment == null) return false;

            _context.ModifierAssignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
