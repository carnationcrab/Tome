using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/modifiers")]
    public class ModifierController: ControllerBase
    {
        private readonly ModifierService _service;

        public ModifierController(ModifierService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all modifiers
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetModifiers()
        {
            var modifiers = await _service.GetAllModifiersAsync();
            return Ok(modifiers);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        /// <summary>
        /// Get a single modifier by ID
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModifierById(Guid id)
        {
            var modifier = await _service.GetModifierByIdAsync(id);
            if (modifier == null) return NotFound();
            return Ok(modifier);
        }

        /// <summary>
        /// Create a new modifier
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateModifier([FromBody] CreateModifierDTO dto)
        {
            var createdModifier = await _service.CreateModifierAsync(dto);
            return CreatedAtAction(nameof(GetModifierById), new { id = createdModifier.id }, createdModifier);
        }

        /// <summary>
        /// Update a modifier
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModifier(Guid id, [FromBody] UpdateModifierDTO dto)
        {
            var success = await _service.UpdateModifierAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete a modifier
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModifier(Guid id)
        {
            var success = await _service.DeleteModifierAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Assign a modifier to an entity (character, character type, item, or field)
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignModifier([FromBody] AssignModifierDTO dto)
        {
            var success = await _service.AssignModifierAsync(dto);
            if (!success) return NotFound();
            return Ok(new { message = "Modifier successfully assigned." });
        }

        /// <summary>
        /// Remove an assigned modifier
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("assign/{assignmentId}")]
        public async Task<IActionResult> RemoveModifierAssignment(Guid assignmentId)
        {
            var success = await _service.RemoveModifierAssignmentAsync(assignmentId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
