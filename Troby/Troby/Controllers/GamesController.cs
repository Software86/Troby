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
    public class GamesController : ControllerBase
    {
        private readonly trobyContext _context;

        public GamesController(trobyContext context)
        {
            _context = context;
        }

        // GET: api/Games
        //Get all games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Games>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        //Get specific game
        [HttpGet("{id}")]
        public async Task<ActionResult<Games>> GetGames(int id)
        {
            var games = await _context.Games.FindAsync(id);

            if (games == null)
            {
                return NotFound();
            }

            return games;
        }

        // POST: api/Games
        //Post new game
        [HttpPost]
        public ActionResult<Games> PostGames(Games game)
        {
            if (game != _context.Games.FirstOrDefault(g => g.Id == game.Id))
            {
                ObjectResult result = new ObjectResult("game_already_exist");
                result.StatusCode = 406;
                return result;
            }
            _context.Games.Add(game);
            _context.SaveChanges();

            return game;
        }

        // PUT: api/Games
        //Edit existing game
        [HttpPut]
        public ActionResult<Games> PutGame(Games game)
        {
            _context.Update(game);
            _context.SaveChanges();

            return game;
        }

        // DELETE: api/Games/5
        //Delete a specific game
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteGames(int id)
        {
            Games game = _context.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            _context.SaveChanges();

            return "game_deleted";
        }

        private bool GamesExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        //GET: api/users/getusergames/asd@asd.se
        //Gets all games that one user owns
        [HttpGet]
        [Route("user/{email}")]
        public ActionResult<IEnumerable<Games>> GetUsersGames(string email)
        {
            List<int> ownerships = _context.Ownership.Where(o => o.UserEmail == email).Select(o => o.GameId).ToList();
            return _context.Games.Where(g => ownerships.Contains(g.Id)).ToList();

        }
    }
}
