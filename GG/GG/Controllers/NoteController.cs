using GG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        ApplicationContext db;
        public NoteController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> Get()
        {
            return await db.Notes.ToListAsync();
        }

        // GET 
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(int id)
        {
            Note note = await db.Notes.FirstOrDefaultAsync(x => x.Id == id);
            if (note == null)
                return NotFound();
            return new ObjectResult(note);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<Note>> Post(Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            db.Notes.Add(note);
            await db.SaveChangesAsync();
            return Ok(note);
        }

        // PUT 
        [HttpPut]
        public async Task<ActionResult<Note>> Put(Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }
            if (!db.Notes.Any(x => x.Id == note.Id))
            {
                return NotFound();
            }

            db.Update(note);
            await db.SaveChangesAsync();
            return Ok(note);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<ActionResult<Note>> Delete(int id)
        {
            Note note = db.Notes.FirstOrDefault(x => x.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            db.Notes.Remove(note);
            await db.SaveChangesAsync();
            return Ok(note);
        }
    }
}
