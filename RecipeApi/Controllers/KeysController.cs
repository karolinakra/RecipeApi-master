using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApi.Models;

namespace RecipeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeysController : ControllerBase
    {
        private readonly RecipeContext _context;

        public KeysController(RecipeContext context)
        {
            _context = context;
        }

        // GET: api/Keys
        [HttpGet]
        public IEnumerable<Key> GetKeys()
        {
            return _context.Keys;
        }

        // GET: api/Keys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKey([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = await _context.Keys.FindAsync(id);

            if (key == null)
            {
                return NotFound();
            }

            return Ok(key);
        }

        // PUT: api/Keys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKey([FromRoute] string id, [FromBody] Key key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != key.Name)
            {
                return BadRequest();
            }

            _context.Entry(key).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KeyExists(id))
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

        // POST: api/Keys
        [HttpPost]
        public async Task<IActionResult> PostKey([FromBody] Key key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Keys.Add(key);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKey", new { id = key.Name }, key);
        }

        // DELETE: api/Keys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKey([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var key = await _context.Keys.FindAsync(id);
            if (key == null)
            {
                return NotFound();
            }

            _context.Keys.Remove(key);
            await _context.SaveChangesAsync();

            return Ok(key);
        }

        private bool KeyExists(string id)
        {
            return _context.Keys.Any(e => e.Name == id);
        }
    }
}