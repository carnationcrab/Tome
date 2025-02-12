using Microsoft.EntityFrameworkCore;
 using Tome.API.Data;
 using Tome.API.Models;
using Tome.API.DTOs;

namespace Tome.API.Services
{
    public class EventService
    {
        private readonly TomeDbContext _context;

        public EventService(TomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventDTO>> GetEventsByUniverseAsync(Guid universeId)
        {
            return await _context.Events
                .Where(e => e.universeId == universeId)
                .Select(e => new EventDTO
                {
                    id = e.id,
                    title = e.title,
                    description = e.description,
                    date = e.date,
                    universeId = e.universeId
                })
            .ToListAsync();
        }

        public async Task<EventDTO> GetEventByIdAsync(Guid id)
        {
            var e = await _context.Events.FindAsync(id);
            if (e == null) return null;

            return new EventDTO
            {
                id = e.id,
                title = e.title,
                description = e.description,
                date = e.date,
                universeId = e.universeId

            };
        }

        public async Task<EventDTO> CreateEventAsync(Guid universeId, CreateEventDTO dto)
        {
            var e = new Event
            {
                universeId = universeId,
                title = dto.title,
                description = dto.description,
                date = dto.date
            };

            _context.Events.Add(e);
            await _context.SaveChangesAsync();

            return new EventDTO
            {
                id = e.id,
                title = e.title,
                description = e.description,
                date = e.date,
                universeId = e.universeId
            };
        }

        public async Task<bool> UpdateEventAsync(Guid id, UpdateEventDTO dto)
        {
            var evnt = await _context.Events.FindAsync(id);
            if (evnt == null) return false;

            evnt.title = dto.title;
            evnt.description = dto.description;
            evnt.date = dto.date;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(Guid id)
        {
            var evnt = await _context.Events.FindAsync(id);
            if (evnt == null) return false;

            _context.Events.Remove(evnt);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
