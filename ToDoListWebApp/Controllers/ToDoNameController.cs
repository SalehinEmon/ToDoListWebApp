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
    public class ToDoNameController : ControllerBase
    {
        private readonly MainDbContext _context;

        public ToDoNameController(MainDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoName
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ToDoName>>> GetToDoNames()
        {
            return await _context.ToDoNames.ToListAsync();
        }

        // GET: api/ToDoName/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ToDoName>> GetToDoName(int id)
        {
            var toDoName = await _context.ToDoNames.FindAsync(id);

            if (toDoName == null)
            {
                return NotFound();
            }

            return toDoName;
        }

        // PUT: api/ToDoName/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutToDoName(int id, ToDoName toDoName)
        {
            if (id != toDoName.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoNameExists(id))
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

        // POST: api/ToDoName
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ToDoName>> PostToDoName(ToDoName toDoName)
        {
            _context.ToDoNames.Add(toDoName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoName", new { id = toDoName.Id }, toDoName);
        }

        // DELETE: api/ToDoName/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ToDoName>> DeleteToDoName(int id)
        {
            var toDoName = await _context.ToDoNames.FindAsync(id);
            if (toDoName == null)
            {
                return NotFound();
            }

            _context.ToDoNames.Remove(toDoName);
            _context.toDoItems.RemoveRange(_context.toDoItems
                .Where(item => item.ToDoNameId == toDoName.Id));
            await _context.SaveChangesAsync();

            return toDoName;
        }

        private bool ToDoNameExists(int id)
        {
            return _context.ToDoNames.Any(e => e.Id == id);
        }
    }
}
