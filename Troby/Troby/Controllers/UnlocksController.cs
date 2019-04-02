using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Troby.Models;

namespace Troby.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnlocksController : ControllerBase
    {
        private readonly trobyContext _context;

        public UnlocksController(trobyContext context)
        {
            _context = context;
        }

        // GET: api/Unlocks
        //Get all unlocks by all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unlocks>>> GetUnlocks()
        {
            return await _context.Unlocks.ToListAsync();
        }

        // GET: api/Unlocks
        //Get all unlocks by a specific user
        [HttpGet]
        [Route("user/{id}")]
        public async Task<ActionResult<IEnumerable<Unlocks>>> GetUnlocksByUser(string id)
        {
            return await _context.Unlocks.Where(u => u.UserId == id).ToListAsync();
            
        }

        // GET: api/Unlocks/5
        // Get a specific unlock
        [HttpGet("{id}")]
        public async Task<ActionResult<Unlocks>> GetUnlocks(int id)
        {
            var unlocks = await _context.Unlocks.FindAsync(id);

            if (unlocks == null)
            {
                return NotFound();
            }

            return unlocks;
        }

        // PUT: api/Unlocks/5
        //Update an unlock
        [HttpPut("{id}")]
        public ActionResult<Unlocks> PutUnlocks(Unlocks unlocks)
        {
            if (_context.Unlocks.FirstOrDefault(u => u.Id == unlocks.Id) == null)
            {
                return NotFound();
            }
            _context.Unlocks.Update(unlocks);

            return unlocks;
        }

        // POST: api/Unlocks
        //Post a new unlock
        [HttpPost]
        public ActionResult<Unlocks> PostUnlocks(Unlocks unlocks)
        {
            _context.Unlocks.Add(unlocks);
            _context.SaveChanges();

            return CreatedAtAction("GetUnlocks", new { id = unlocks.Id }, unlocks);
        }

        // DELETE: api/Unlocks/5
        //Delete an unlock
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUnlocks(int id)
        {
            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            _context.SaveChanges();

            return $"{id} has been deleted";
        }
    }
}
