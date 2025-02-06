﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversesController : ControllerBase
    {
        private readonly UniverseService _service;

        public UniversesController(UniverseService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniverseDTO>>> GetUniverses()
        {
            var universes = await _service.GetAllUniversesAsync();
            return Ok(universes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UniverseDTO>> GetUniverse(Guid id)
        {
            var universe = await _service.GetUniverseByIdAsync(id);
            if (universe == null)
                return NotFound();
            return Ok(universe);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<UniverseDTO>> CreateUniverse(CreateUniverseDTO dto)
        {
            var createdUniverse = await _service.CreateUniverseAsync(dto);
            return CreatedAtAction(nameof(GetUniverse), new { id = createdUniverse.id }, createdUniverse);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUniverse(Guid id, UniverseDTO dto)
        {
            var updated = await _service.UpdateUniverseAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniverse(Guid id)
        {
            var deleted = await _service.DeleteUniverseAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }

}
