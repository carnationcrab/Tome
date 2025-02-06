using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/universes/{universeId}/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventService _service;

        public EventsController(EventService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetEvents(Guid universeId)
        {
            var events = await _service.GetEventsByUniverseAsync(universeId);
            return Ok(events);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetEvent(Guid id)
        {
            var evnt = await _service.GetEventByIdAsync(id);
            if (evnt == null)
                return NotFound();
            return Ok(evnt);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EventDTO>> CreateEvent(Guid universeId, CreateEventDTO dto)
        {
            var createdEvent = await _service.CreateEventAsync(universeId, dto);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.id }, createdEvent);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, UpdateEventDTO dto)
        {
            var updated = await _service.UpdateEventAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var deleted = await _service.DeleteEventAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }

}
