using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListWebApp.Model;

namespace ToDoListWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly MainDbContext _context;

        public ToDoItemController(MainDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItem
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ToDoItem>>> GettoDoItems()
        //{
        //    return await _context.toDoItems.ToListAsync();
        //}

        //GET: api/ToDoItem/5
        [HttpGet("all/{toDoNameId}")]
        [Authorize]


        public async Task<ActionResult<IEnumerable<ToDoItem>>> GettoDoItems(int toDoNameId)
        {
            var toDoItem = await _context.toDoItems
                .Where(t => t.ToDoNameId == toDoNameId)
                .ToListAsync();

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ToDoItem>> GettoDoItem(int id)
        {
            ToDoItem toDoItem = await _context.toDoItems.
                FirstOrDefaultAsync(td => td.Id == id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;

        }


        // PUT: api/ToDoItem/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/ToDoItem
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
            _context.toDoItems.Add(toDoItem);
            int roweffectd = await _context.SaveChangesAsync();

            return CreatedAtAction("GettoDoItem", new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/ToDoItem/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ToDoItem>> DeleteToDoItem(int id)
        {
            var toDoItem = await _context.toDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.toDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ToDoItemExists(int id)
        {
            return _context.toDoItems.Any(e => e.Id == id);
        }
    }
}
