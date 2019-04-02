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
    public class OwnershipsController : ControllerBase
    {
        private readonly trobyContext _context;

        public OwnershipsController(trobyContext context)
        {
            _context = context;
        }

        // GET: api/Ownerships
        //Get all ownerships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ownership>>> GetOwnership()
        {
            return await _context.Ownership.ToListAsync();
        }

        // GET: api/Ownerships/5
        //Get all ownerships from one user
        [HttpGet("{id}")]
        public async Task<ActionResult<Ownership>> GetOwnership(int id)
        {
            var ownership = await _context.Ownership.FindAsync(id);

            if (ownership == null)
            {
                return NotFound();
            }

            return ownership;
        }

        // POST: api/Ownerships
        //Post new ownership
        [HttpPost]
        public ActionResult<Ownership> PostOwnership(Ownership ownership)
        {
            if (OwnershipExists(ownership.Id))
            {
                ObjectResult o = new ObjectResult("ownership_has_already_been_set")
                {
                    StatusCode = 400
                };
            }

            _context.Ownership.Add(ownership);

             _context.SaveChanges();

            return ownership;
            
        }

        // DELETE: api/Ownerships/5
        //Delete ownership
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteOwnership(int id)
        {
            Ownership own = _context.Ownership.Find(id);
            if (own == null)
            {
                return NotFound();
            }

            _context.Ownership.Remove(own);
            _context.SaveChanges();

            return "ownership_deleted";
        }

        private bool OwnershipExists(int id)
        {
            return _context.Ownership.Any(e => e.GameId == id);
        }
    }
}
