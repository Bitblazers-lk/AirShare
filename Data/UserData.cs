using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirShare
{
    public class UserData
    {
        public string Token { get; set; }

        public User Auth()
        {
            return Core.AuthToken(Token);
        }

        public bool Validate(string path)
        {
            return Auth().Validate(path);
        }

    }
}