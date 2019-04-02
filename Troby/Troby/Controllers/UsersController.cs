using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Troby.Models;
using Troby.Data;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Troby.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly trobyContext _context;
        private readonly IConfiguration _config;
        private readonly DbHandlerUsers _db;


        public UsersController(trobyContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _db = new DbHandlerUsers(context, config);
        }

        // GET: api/Users
        //Get all users that exists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _db.GetAllAsync();
        }

        // GET: api/Users/score
        //Get total score of user
        [HttpGet]
        [Route("score/{id}")]
        public async Task<ActionResult<int>> GetTotalScore(string id)
        {
            var unlocks = _context.Unlocks.Where(u => u.UserId == id).Select(s => s.AchId);
            List<int> totalList = _context.Achievements.Where(a => unlocks.Contains(a.Id)).Select(r => r.Score).ToList();
            return totalList.Sum();

        }

        // GET: api/Users/kalle@asd.se
        //Gets a specific user
        [HttpGet("{email}")]
        public async Task<ActionResult<Users>> GetUser(string email)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(usr => usr.Email == email);
            if (user == null) return NotFound();
            return user;
        }

        // POST: api/Users
        //Add new user
        [HttpPost]
        public ActionResult<string> PostUsers([FromForm]string email, [FromForm]string pass)
        {
            return _db.CreateNewUser(email, pass);
        }

        // POST: api/Users
        //Login (get username and a key)
        [HttpPost]
        [Route("Login")]
        public ActionResult<string> LoginUser([FromForm]string email, [FromForm]string pass)
        {
            return _db.Login(email, pass);
        }

        // PUT: api/Users
        //Change the password for one user
        [HttpPut]
        public ActionResult<string> ChangePassword([FromForm]string email, [FromForm]string pass, [FromForm] string newPassword)
        {
            if (_db.CheckPassword(email, pass))
            {
                return _db.UpdatePassword(email, newPassword);
            }
            else
            {
                ObjectResult o = new ObjectResult("wrong_email_or_password")
                {
                    StatusCode = 401
                };
                return o;
            }
        }

        // DELETE: api/Users/5
        //Delete one user
        [HttpDelete("{email}")]
        public ActionResult<string> DeleteUsers(string email)
        {
            var users = _context.Users.Find(email);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            _context.SaveChanges();

            return $"{email} has been deleted";
        }



    }
}
