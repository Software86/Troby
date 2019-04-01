using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Troby.models;
using Troby.Models;

namespace Troby.Data
{
    public class DbHandler
    {
        private readonly trobyContext _db;
        private readonly IConfiguration _config;

        public DbHandler(trobyContext cont, IConfiguration config)
        {
            _db = cont;
            _config = config;
        }

        public async Task<ActionResult<IEnumerable<Users>>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public ActionResult<Users> CreateNewUser(string username, string password)
        {
            if (!UsernameIsValid(username))
            {
                ObjectResult result = new ObjectResult("email_Not_Valid");
                result.StatusCode = 406;
                return result;
            }
            if (UserExist(username))
            {
                ObjectResult result = new ObjectResult("user_already_exist");
                result.StatusCode = 406;
                return result;
            }
            Hasher hasher = new Hasher(password);
            string hash = hasher.ComputeHash();
            string salt = hasher.salt;
            Users user = new Users { Email = username, Salt = salt, Hash = hash };
            _db.Users.Add(user);
            _db.SaveChanges();

            return user;
        }

        public ActionResult<Key> Login(string username, string pass)
        {
            if (!UserExist(username))
            {
                ObjectResult result = new ObjectResult("user_does_not_exist");
                result.StatusCode = 400;
                return result;
            }
            Users user = _db.Users.AsNoTracking().FirstOrDefault(u => u.Email == username);
            Hasher hasher = new Hasher(pass,user.Salt);
            string guessHash = hasher.ComputeHash();
            if (guessHash.Equals(user.Hash))
            {
                return new Key() { username = username, key = 555 };
            }
            else
            {
                ObjectResult res = new ObjectResult("wrong_password");
                res.StatusCode = 401;
                return res;
            }
        }

        private bool UsernameIsValid(string username)
        {
            string regexString = _config.GetSection("regex")["username"];
            Regex regex = new Regex(regexString);
            return regex.IsMatch(username);
        }

        public bool UserExist(string email)
        {
            return _db.Users.Any(e => e.Email == email);
        }

    }
}
