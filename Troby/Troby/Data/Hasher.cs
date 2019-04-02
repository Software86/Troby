using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Troby.Data  
{
    public class Hasher
    {
        private byte[] _saltByte { get; set; }
        private string _password;
        public string salt { get; set; }
        
        public Hasher()
        {}

        public Hasher(string pass)
        {
            _password = pass;
        }

        public Hasher(string pass, string salt)
        {
            _password = pass;
            this.salt = salt;
            _saltByte = Convert.FromBase64String(this.salt);
        }

        public string ComputeHash()
        {
            if (salt == null)
            {
                _saltByte = GenerateNewSaltByte(32);
                salt = Convert.ToBase64String(_saltByte);
            }
            byte[] hash = KeyDerivation.Pbkdf2(_password, _saltByte, KeyDerivationPrf.HMACSHA512, 10000, 32);
            return Convert.ToBase64String(hash);
        }

        public byte[] GenerateNewSaltByte(int size)
        {
            byte[] salt = new byte[size];
            Random rand = new Random();
            rand.NextBytes(salt);
            return salt;
        }
        
        

        
    }
}
