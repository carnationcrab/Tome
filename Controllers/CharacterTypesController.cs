﻿using Microsoft.AspNetCore.Mvc;
using Tome.DTOs;
using Tome.Services;

namespace Tome.Controllers
{
    [ApiController]
    [Route("api/universes/{universeId}/charactertypes")]
    public class CharacterTypesController : ControllerBase
    {
        private readonly CharacterTypeService _characterTypeService;

        public CharacterTypesController(CharacterTypeService characterTypeService)
        {
            _characterTypeService = characterTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacterTypes(Guid universeId)
        {
            var types = await _characterTypeService.GetCharacterTypesByUniverseIdAsync(universeId);
            return Ok(types);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacterType(Guid universeId, [FromBody] CreateCharacterTypeDTO dto)
        {
            var createdType = await _characterTypeService.CreateCharacterTypeAsync(universeId, dto);
            return CreatedAtAction(nameof(GetCharacterTypes), new { universeId }, createdType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterType(Guid universeId, Guid id)
        {
            var success = await _characterTypeService.DeleteCharacterTypeAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

    }
}
