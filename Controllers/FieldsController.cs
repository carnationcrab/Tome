﻿using Microsoft.AspNetCore.Mvc;
using Tome.DTOs;
using Tome.Services;

namespace Tome.Controllers
{
    [ApiController]
    [Route("api/charactertypes/{characterTypeId}/fields")]
    public class FieldsController : ControllerBase
    {
        private readonly FieldService _fieldService;

        public FieldsController(FieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFields(Guid characterTypeId)
        {
            var fields = await _fieldService.GetFieldsByCharacterTypeIdAsync(characterTypeId);
            return Ok(fields);
        }

        [HttpPost]
        public async Task<IActionResult> CreateField(Guid characterTypeId, [FromBody] CreateFieldDTO dto)
        {
            var createdField = await _fieldService.CreateFieldAsync(characterTypeId, dto);
            return CreatedAtAction(nameof(GetFields), new { characterTypeId }, createdField);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateField(Guid id, [FromBody] UpdateFieldDTO dto)
        {
            var success = await _fieldService.UpdateFieldAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(Guid id)
        {
            var success = await _fieldService.DeleteFieldAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
