using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/modifierassignments")]
    public class ModifierAssignmentController : ControllerBase
    {
        private readonly ModifierAssignmentService _service;

        public ModifierAssignmentController(ModifierAssignmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all modifier assignments
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAllModifierAssignments()
        {
            var assignments = await _service.GetAllModifierAssignmentsAsync();
            return Ok(assignments);
        }

        /// <summary>
        /// Get a specific modifier assignment by ID
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModifierAssignment(Guid id)
        {
            var assignment = await _service.GetModifierAssignmentByIdAsync(id);
            if (assignment == null) return NotFound();
            return Ok(assignment);
        }

        /// <summary>
        /// Assign a modifier to a character, character type, item, or field
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AssignModifier([FromBody] CreateModifierAssignmentDTO dto)
        {
            var assignment = await _service.AssignModifierAsync(dto);
            return CreatedAtAction(nameof(GetModifierAssignment), new { id = assignment.id }, assignment);
        }

        /// <summary>
        /// Remove a modifier assignment
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveModifierAssignment(Guid id)
        {
            var success = await _service.RemoveModifierAssignmentAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
