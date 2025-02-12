using Microsoft.EntityFrameworkCore;
using Tome.API.Data;
using Tome.API.DTOs;
using Tome.API.Models;

namespace Tome.API.Services
{
    public class ItemService
    {
        private readonly TomeDbContext _context;

        public ItemService(TomeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all items
        /// </summary>
        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            return await _context.Items
                .Select(i => new ItemDTO
                {
                    id = i.id,
                    name = i.name,
                    description = i.description
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get a single item by ID
        /// </summary>
        public async Task<ItemDTO?> GetItemByIdAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return null;

            return new ItemDTO
            {
                id = item.id,
                name = item.name,
                description = item.description
            };
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        public async Task<ItemDTO> CreateItemAsync(CreateItemDTO dto)
        {
            var item = new Item
            {
                id = Guid.NewGuid(),
                name = dto.name,
                description = dto.description
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return new ItemDTO
            {
                id = item.id,
                name = item.name,
                description = item.description
            };
        }

        /// <summary>
        /// Update an item
        /// </summary>
        public async Task<bool> UpdateItemAsync(Guid id, UpdateItemDTO dto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;

            item.name = dto.name;
            item.description = dto.description;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
