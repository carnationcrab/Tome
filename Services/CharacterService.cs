//using Tome.API.DTOs;
//using Tome.API.Models;

//namespace Tome.API.Services
//{
//    public class CharacterService
//    {
//        private static readonly List<Character> _mockCharacters = new List<Character>
//        {
//            new Character { Id = Guid.NewGuid(), UniverseId = Guid.NewGuid(), Name = "Mock Character 1", Description = "A temporary character.", Attributes = "{}" },
//            new Character { Id = Guid.NewGuid(), UniverseId = Guid.NewGuid(), Name = "Mock Character 2", Description = "Another placeholder character.", Attributes = "{}" }
//        };

//        public async Task<IEnumerable<CharacterDTO>> GetCharactersByUniverseAsync(Guid universeId)
//        {
//            return await Task.FromResult(
//                _mockCharacters
//                    .Where(c => c.UniverseId == universeId)
//                    .Select(c => new CharacterDTO
//                    {
//                        Id = c.Id,
//                        Name = c.Name,
//                        Description = c.Description,
//                        Attributes = c.Attributes
//                    })
//            );
//        }

//        public async Task<CharacterDTO> GetCharacterByIdAsync(Guid id)
//        {
//            var character = _mockCharacters.FirstOrDefault(c => c.Id == id);
//            if (character == null) return null;

//            return await Task.FromResult(new CharacterDTO
//            {
//                Id = character.Id,
//                Name = character.Name,
//                Description = character.Description,
//                Attributes = character.Attributes
//            });
//        }

//        public async Task<CharacterDTO> CreateCharacterAsync(Guid universeId, CreateCharacterDTO dto)
//        {
//            var newCharacter = new Character
//            {
//                Id = Guid.NewGuid(),
//                UniverseId = universeId,
//                Name = dto.Name,
//                Description = dto.Description,
//                Attributes = dto.Attributes
//            };

//            _mockCharacters.Add(newCharacter);

//            return await Task.FromResult(new CharacterDTO
//            {
//                Id = newCharacter.Id,
//                Name = newCharacter.Name,
//                Description = newCharacter.Description,
//                Attributes = newCharacter.Attributes
//            });
//        }

//        public async Task<bool> UpdateCharacterAsync(Guid id, UpdateCharacterDTO dto)
//        {
//            var character = _mockCharacters.FirstOrDefault(c => c.Id == id);
//            if (character == null) return await Task.FromResult(false);

//            character.Name = dto.Name;
//            character.Description = dto.Description;
//            character.Attributes = dto.Attributes;

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> DeleteCharacterAsync(Guid id)
//        {
//            var character = _mockCharacters.FirstOrDefault(c => c.Id == id);
//            if (character == null) return await Task.FromResult(false);

//            _mockCharacters.Remove(character);
//            return await Task.FromResult(true);
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.DTOs;
using Tome.API.Models;

namespace Tome.API.Services
{
    public class CharacterService
    {
        private readonly TomeDbContext _context;

        public CharacterService(TomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CharacterDTO>> GetCharactersByUniverseAsync(Guid universeId)
        {
            return await _context.Characters
                .Where(c => c.universeId == universeId)
                .Select(c => new CharacterDTO
                {
                    id = c.id,
                    name = c.name,
                    description = c.description,
                    attributes = c.attributes
                })
            .ToListAsync();
        }

        public async Task<CharacterDTO?> GetCharacterByIdAsync(Guid id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return null;

            return new CharacterDTO
            {
                id = character.id,
                name = character.name,
                description = character.description,
                attributes = character.attributes
            };
        }

        public async Task<CharacterDTO> CreateCharacterAsync(Guid universeId, CreateCharacterDTO dto)
        {
            var character = new Character
            {
                universeId = universeId,
                name = dto.name,
                description = dto.description,
                attributes = dto.attributes
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return new CharacterDTO
            {
                id = character.id,
                name = character.name,
                description = character.description,
                attributes = character.attributes
            };
        }

        public async Task<bool> UpdateCharacterAsync(Guid id, UpdateCharacterDTO dto)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return false;

            character.name = dto.name;
            character.description = dto.description;
            character.attributes = dto.attributes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCharacterAsync(Guid id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null) return false;

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
