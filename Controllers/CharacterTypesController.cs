using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/charactertypes")]
    //[Route("api/universes/{universeId}/charactertypes")]

    public class CharacterTypesController : ControllerBase
    {
        private readonly CharacterTypeService _characterTypeService;

        public CharacterTypesController(CharacterTypeService characterTypeService)
        {
            _characterTypeService = characterTypeService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetCharacterTypes(Guid universeId)
        {
            var types = await _characterTypeService.GetCharacterTypesByUniverseIdAsync(universeId);
            return Ok(types);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCharacterTypeById(Guid id)
        {
            var characterType = await _characterTypeService.GetCharacterTypeByIdAsync(id);
            if (characterType == null)
                return NotFound(new { message = "Character type not found" });

            return Ok(characterType);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateCharacterType(Guid universeId, [FromBody] CreateCharacterTypeDTO dto)
        {
            var createdType = await _characterTypeService.CreateCharacterTypeAsync(dto);
            return CreatedAtAction(nameof(GetCharacterTypes), new { universeId }, createdType);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacterType(Guid universeId, Guid id)
        {
            var success = await _characterTypeService.DeleteCharacterTypeAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}/visibility")]
        public async Task<IActionResult> UpdateVisibility(Guid universeId, Guid id, [FromBody] UpdateCharacterTypeVisibilityDTO model)
        {
            var success = await _characterTypeService.SetCharacterTypeVisibilityAsync(id, model.visibility);
            if (!success)
                return NotFound(new { message = "Character type not found or update failed." });

            return Ok(new { message = "Character type visibility updated successfully." });
        }
    }
}
