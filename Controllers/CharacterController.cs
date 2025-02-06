using Microsoft.AspNetCore.Mvc;
using Tome.API.API.DTOs;
using Tome.API.API.Services;

namespace Tome.API.API.Controllers
{
    [ApiController]
    [Route("api/universes/{universeId}/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly CharacterService _service;

        public CharactersController(CharacterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterDTO>>> GetCharacters(Guid universeId)
        {
            var characters = await _service.GetCharactersByUniverseAsync(universeId);
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterDTO>> GetCharacter(Guid id)
        {
            var character = await _service.GetCharacterByIdAsync(id);
            if (character == null)
                return NotFound();
            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<CharacterDTO>> CreateCharacter(Guid universeId, CreateCharacterDTO dto)
        {
            var createdCharacter = await _service.CreateCharacterAsync(universeId, dto);
            return CreatedAtAction(nameof(GetCharacter), new { id = createdCharacter.id }, createdCharacter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCharacter(Guid id, UpdateCharacterDTO dto)
        {
            var updated = await _service.UpdateCharacterAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(Guid id)
        {
            var deleted = await _service.DeleteCharacterAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }

}
