using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Troby.models;
using Troby.Data;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Troby.Models;

namespace Troby.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly trobyContext _context;
        private readonly IConfiguration _config;
        private readonly DbHandler _db;

        public UsersController(trobyContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _db = new DbHandler(context, config);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _db.GetAllAsync();
            
        }

        // GET: api/Users/kalle@asd.se
        [HttpGet("{email}")]
        public async Task<ActionResult<Users>> GetUser(string email)
        {
            Users user = await _context.Users.FirstOrDefaultAsync(usr => usr.Email == email);
            if (user == null) return NotFound();
            return user;
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<Users> PostUsers([FromForm]string email, [FromForm]string pass)
        {
            return _db.CreateNewUser(email, pass);
        }

        // POST: api/Users
        [HttpPost]
        [Route("Login")]
        public ActionResult<Key> LoginUser([FromForm]string email, [FromForm]string pass)
        {
            return _db.Login(email, pass);
        }

        //put: api/users/5
        //[httpput("{id}")]
        //public async task<iactionresult> putusers(string id, users users)
        //{

        //    await _context.users.addasync(user);

        //    if (id != users.email)
        //    {
        //        return badrequest();
        //    }

        //    _context.entry(users).state = entitystate.modified;

        //    try
        //    {
        //        await _context.savechangesasync();
        //    }
        //    catch (dbupdateconcurrencyexception)
        //    {
        //        if (!usersexists(id))
        //        {
        //            return notfound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return nocontent();
        //}

        // DELETE: api/Users/5
        [HttpDelete("{email}")]
        public ActionResult<bool> DeleteUsers(string email)
        {
            var users = _context.Users.Find(email);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            _context.SaveChangesAsync();

            return true;
        }
        
        
    }
}
