using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComentariosApi.Models;

namespace ComentariosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioItemsController : ControllerBase
    {
        private readonly ComentarioContext _context;

        public ComentarioItemsController(ComentarioContext context)
        {
            _context = context;
        }

        // GET: api/ComentarioItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComentarioItemDTO>>> GetComentarioItems()
        {
            return await _context.ComentarioItems
            .Select(x => ItemCoDTO(x)).ToListAsync();
        }

        // GET: api/ComentarioItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComentarioItemDTO>> GetComentarioItem(long id)
        {
            var comentarioItem = await _context.ComentarioItems.FindAsync(id);

            if (comentarioItem == null)
            {
                return NotFound();
            }

            return ItemCoDTO(comentarioItem);
        }

        // PUT: api/ComentarioItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentarioItem(long id, ComentarioItemDTO comentarioDTO)
        {
            if (id != comentarioDTO.Id)
            {
                return BadRequest();
            }
            var comentarioItem = await _context.ComentarioItems.FindAsync(id);
            if(comentarioItem == null)
            {
                return NotFound();
            }

            comentarioItem.Name = comentarioDTO.Name;
            comentarioItem.IsComplete = comentarioDTO.IsComplete;
            comentarioItem.Text = comentarioDTO.Text;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ComentarioItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/ComentarioItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComentarioItemDTO>> PostComentarioItem(ComentarioItemDTO comentarioItemDTO)
        {
            var comentarioItem = new ComentarioItem
            {
                IsComplete = comentarioItemDTO.IsComplete,
                Name = comentarioItemDTO.Name, 
                Text = comentarioItemDTO.Text
            };
            _context.ComentarioItems.Add(comentarioItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetComentarioItem),
                new{ id = comentarioItem.Id}, 
                ItemCoDTO(comentarioItem));
        }

        // DELETE: api/ComentarioItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentarioItem(long id)
        {
            var comentarioItem = await _context.ComentarioItems.FindAsync(id);
            if (comentarioItem == null)
            {
                return NotFound();
            }

            _context.ComentarioItems.Remove(comentarioItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentarioItemExists(long id)
        {
            return _context.ComentarioItems.Any(e => e.Id == id);
        }

        private static ComentarioItemDTO ItemCoDTO(ComentarioItem comentarioItem) =>
            new ComentarioItemDTO
            {
                Id = comentarioItem.Id,
                Name = comentarioItem.Name,
                IsComplete = comentarioItem.IsComplete,
                Text = comentarioItem.Text
            };
    }
}
