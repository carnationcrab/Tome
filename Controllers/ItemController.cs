using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tome.API.DTOs;
using Tome.API.Services;

namespace Tome.API.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _service;

        public ItemsController(ItemService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all items
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _service.GetAllItemsAsync();
            return Ok(items);
        }

        /// <summary>
        /// Get a specific item by ID
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            var item = await _service.GetItemByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemDTO dto)
        {
            var createdItem = await _service.CreateItemAsync(dto);
            return CreatedAtAction(nameof(GetItemById), new { id = createdItem.id }, createdItem);
        }

        /// <summary>
        /// Update an item
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemDTO dto)
        {
            var updated = await _service.UpdateItemAsync(id, dto);
            if (!updated)
                return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var deleted = await _service.DeleteItemAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
