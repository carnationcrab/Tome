using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetCharacterTypes(Guid universeId)
        {
            var types = await _characterTypeService.GetCharacterTypesByUniverseIdAsync(universeId);
            return Ok(types);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateCharacterType(Guid universeId, [FromBody] CreateCharacterTypeDTO dto)
        {
            var createdType = await _characterTypeService.CreateCharacterTypeAsync(universeId, dto);
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

    }
}
