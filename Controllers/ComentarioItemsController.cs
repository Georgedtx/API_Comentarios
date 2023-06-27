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
        public async Task<ActionResult<IEnumerable<ComentarioItem>>> GetComentarioItems()
        {
          if (_context.ComentarioItems == null)
          {
              return NotFound();
          }
            return await _context.ComentarioItems.ToListAsync();
        }

        // GET: api/ComentarioItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComentarioItem>> GetComentarioItem(long id)
        {
          if (_context.ComentarioItems == null)
          {
              return NotFound();
          }
            var comentarioItem = await _context.ComentarioItems.FindAsync(id);

            if (comentarioItem == null)
            {
                return NotFound();
            }

            return comentarioItem;
        }

        // PUT: api/ComentarioItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentarioItem(long id, ComentarioItem comentarioItem)
        {
            if (id != comentarioItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(comentarioItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ComentarioItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComentarioItem>> PostComentarioItem(ComentarioItem comentarioItem)
        {
          if (_context.ComentarioItems == null)
          {
              return Problem("Entity set 'ComentarioContext.ComentarioItems'  is null.");
          }
            _context.ComentarioItems.Add(comentarioItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentarioItem", new { id = comentarioItem.Id }, comentarioItem);
        }

        // DELETE: api/ComentarioItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentarioItem(long id)
        {
            if (_context.ComentarioItems == null)
            {
                return NotFound();
            }
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
            return (_context.ComentarioItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
