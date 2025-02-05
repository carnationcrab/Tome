//using Tome.DTOs;
//using Tome.Models;

//namespace Tome.Services
//{
//    public class EventService
//    {
//        private static readonly List<Event> _mockEvents = new List<Event>
//        {
//            new Event { Id = Guid.NewGuid(), UniverseId = Guid.NewGuid(), Title = "Mock Event 1", Description = "A placeholder event.", Date = DateTime.UtcNow },
//            new Event { Id = Guid.NewGuid(), UniverseId = Guid.NewGuid(), Title = "Mock Event 2", Description = "Another mock event.", Date = DateTime.UtcNow }
//        };

//        public async Task<IEnumerable<EventDTO>> GetEventsByUniverseAsync(Guid universeId)
//        {
//            return await Task.FromResult(
//                _mockEvents
//                    .Where(e => e.UniverseId == universeId)
//                    .Select(e => new EventDTO
//                    {
//                        Id = e.Id,
//                        Title = e.Title,
//                        Description = e.Description,
//                        Date = e.Date,
//                        UniverseId = e.UniverseId
//                    })
//            );
//        }

//        public async Task<EventDTO> GetEventByIdAsync(Guid id)
//        {
//            var evnt = _mockEvents.FirstOrDefault(e => e.Id == id);
//            if (evnt == null) return null;

//            return await Task.FromResult(new EventDTO
//            {
//                Id = evnt.Id,
//                Title = evnt.Title,
//                Description = evnt.Description,
//                Date = evnt.Date,
//                UniverseId = evnt.UniverseId
//            });
//        }

//        public async Task<EventDTO> CreateEventAsync(Guid universeId, CreateEventDTO dto)
//        {
//            var newEvent = new Event
//            {
//                Id = Guid.NewGuid(),
//                UniverseId = universeId,
//                Title = dto.Title,
//                Description = dto.Description,
//                Date = dto.Date
//            };

//            _mockEvents.Add(newEvent);

//            return await Task.FromResult(new EventDTO
//            {
//                Id = newEvent.Id,
//                Title = newEvent.Title,
//                Description = newEvent.Description,
//                Date = newEvent.Date,
//                UniverseId = newEvent.UniverseId
//            });
//        }

//        public async Task<bool> UpdateEventAsync(Guid id, UpdateEventDTO dto)
//        {
//            var evnt = _mockEvents.FirstOrDefault(e => e.Id == id);
//            if (evnt == null) return await Task.FromResult(false);

//            evnt.Title = dto.Title;
//            evnt.Description = dto.Description;
//            evnt.Date = dto.Date;

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> DeleteEventAsync(Guid id)
//        {
//            var evnt = _mockEvents.FirstOrDefault(e => e.Id == id);
//            if (evnt == null) return await Task.FromResult(false);

//            _mockEvents.Remove(evnt);
//            return await Task.FromResult(true);
//        }
//    }
//}


 using Microsoft.EntityFrameworkCore;
 using Tome.Data;
 using Tome.DTOs;
 using Tome.Models;

 namespace Tome.Services
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
