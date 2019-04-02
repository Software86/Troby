using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Troby.Models;

namespace Troby.Data
{
    public class DbHandlerUsers
    {
        private readonly trobyContext _db;
        private readonly IConfiguration _config;

        public DbHandlerUsers(trobyContext cont, IConfiguration config)
        {
            _db = cont;
            _config = config;
        }

        public async Task<ActionResult<IEnumerable<Users>>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public ActionResult<string> CreateNewUser(string username, string password)
        {
            if (username == null || password == null)
            {
                ObjectResult objRes = new ObjectResult("invalid_parameters")
                {
                    StatusCode = 401
                };
                return objRes;
            }
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
            Users user;
            Hasher hasher = new Hasher(password);
            string hash = hasher.ComputeHash();
            string salt = hasher.salt;
            user = new Users { Email = username, Salt = salt, Hash = hash };

            _db.Users.Add(user);
            _db.SaveChanges();

            return $"{username} has been created";
        }

        public ActionResult<string> Login(string username, string pass)
        {
            if (!UserExist(username))
            {
                ObjectResult result = new ObjectResult("user_does_not_exist");
                result.StatusCode = 400;
                return result;
            }
            if (CheckPassword(username, pass))
            {
                return username;
            }
            else
            {
                ObjectResult result = new ObjectResult("wrong_password");
                result.StatusCode = 401;
                return result;
            }
        }

        public ActionResult<string> UpdatePassword(string username, string newPassword)
        {
            Users user = _db.Users.FirstOrDefault(u => u.Email == username);
            if (user == null)
            {
                ObjectResult result = new ObjectResult("user_does_not_exist");
                result.StatusCode = 400;
            }
            Hasher hasher = new Hasher(newPassword);
            newPassword = hasher.ComputeHash();
            user.Hash = newPassword;
            user.Salt = hasher.salt;


            _db.Users.Update(user);
            _db.SaveChanges();
            return "OK";
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

        public bool CheckPassword(string username, string pass)
        {
            Users user = _db.Users.AsNoTracking().FirstOrDefault(u => u.Email == username);
            string guessHash;
            Hasher hasher = new Hasher(pass, user.Salt);
            guessHash = hasher.ComputeHash();
            if (guessHash.Equals(user.Hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
