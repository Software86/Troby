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
    public class AchievementsController : ControllerBase
    {
        private readonly trobyContext _context;

        public AchievementsController(trobyContext context)
        {
            _context = context;
        }

        // GET: api/Achievements
        //Get all achievements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Achievements>>> GetAchievements()
        {
            return await _context.Achievements.ToListAsync();
        }

        // GET: api/Achievements/
        //Get all achievements from a specific game
        [HttpGet]
        [Route("game/{id}")]
        public async Task<ActionResult<IEnumerable<Achievements>>> GetAchievementsFromGame(int id)
        {
            return await _context.Achievements.Where(u => u.GameId == id).ToListAsync();
        }

        // GET: api/Achievements/5
        //Get a specific achievement
        [HttpGet("{id}")]
        public ActionResult<Achievements> GetAchievements(int id)
        {
            var achievements = _context.Achievements.Find(id);

            if (achievements == null)
            {
                return NotFound();
            }

            return achievements;
        }

        // POST: api/Achievements
        //Post a new Achievement
        [HttpPost]
        public ActionResult<Achievements> PostAchievements(Achievements achievements)
        {
            _context.Achievements.Add(achievements);
            _context.SaveChanges();

            return achievements;
        }

        // PUT: api/Achievements
        //Update a Achievement
        [HttpPut]
        public ActionResult<Achievements> PutAchievement(Achievements achievements)
        {
            _context.Achievements.Update(achievements);
            _context.SaveChanges();

            return achievements;
        }

        // DELETE: api/Achievements/5
        //Delete an achievement
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteAchievements(int id)
        {
            var achi = _context.Achievements.Find(id);
            if (achi == null)
            {
                return NotFound();
            }

            _context.Achievements.Remove(achi);
            _context.SaveChanges();

            return $"User with id: {id} has been deleted";
        }

        private bool AchievementsExists(int id)
        {
            return _context.Achievements.Any(e => e.Id == id);
        }


    }
}
