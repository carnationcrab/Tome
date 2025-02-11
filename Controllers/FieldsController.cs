using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/fields")]
    public class FieldsController : ControllerBase
    {
        private readonly FieldService _fieldService;

        public FieldsController(FieldService fieldService)
        {
            _fieldService = fieldService;
        }

        /// <summary>
        /// Get all global fields.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetFields()
        {
            var fields = await _fieldService.GetAllFieldsAsync();
            return Ok(fields);
        }

        /// <summary>
        /// Get a single field by ID.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFieldById(Guid id)
        {
            var field = await _fieldService.GetFieldByIdAsync(id);
            if (field == null) return NotFound();
            return Ok(field);
        }

        /// <summary>
        /// Create a new field.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateField([FromBody] CreateFieldDTO dto)
        {
            var createdField = await _fieldService.CreateFieldAsync(dto);
            return CreatedAtAction(nameof(GetFieldById), new { id = createdField.id }, createdField);
        }

        /// <summary>
        /// Update a field.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateField(Guid id, [FromBody] UpdateFieldDTO dto)
        {
            var success = await _fieldService.UpdateFieldAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Delete a field.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(Guid id)
        {
            var success = await _fieldService.DeleteFieldAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Get all fields assigned to a Character Type.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("charactertypes/{characterTypeId}")]
        public async Task<IActionResult> GetFieldsByCharacterType(Guid characterTypeId)
        {
            var fields = await _fieldService.GetFieldsByCharacterTypeIdAsync(characterTypeId);
            return Ok(fields);
        }

        /// <summary>
        /// Assign an existing field to a Character Type.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("charactertypes/{characterTypeId}")]
        public async Task<IActionResult> AssignFieldToCharacterType(Guid characterTypeId, [FromBody] AssignFieldDTO dto)
        {
            var success = await _fieldService.AssignFieldToCharacterTypeAsync(characterTypeId, dto.fieldId);
            if (!success) return NotFound();
            return Ok(new { message = "Field successfully assigned to Character Type" });
        }

        /// <summary>
        /// Get all fields (including custom) for a character.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("characters/{characterId}")]
        public async Task<IActionResult> GetFieldsByCharacter(Guid characterId)
        {
            var fields = await _fieldService.GetFieldsByCharacterIdAsync(characterId);
            return Ok(fields);
        }

        /// <summary>
        /// Add a custom field to a character.
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("characters/{characterId}")]
        public async Task<IActionResult> AddCustomFieldToCharacter(Guid characterId, [FromBody] AddCustomFieldDTO dto)
        {
            var createdField = await _fieldService.AddCustomFieldToCharacterAsync(characterId, dto);
            if (createdField == null) return BadRequest(new { message = "Field creation failed." });

            return Ok(createdField);
        }
    }
}
