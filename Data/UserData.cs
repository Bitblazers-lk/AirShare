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


    public class AuthDetails
    {
        public Dictionary<string, User> Pars { get; set; } = new Dictionary<string, User>();
    }
    public class User
    {
        public string Name { get; set; }
        public string HPass { get; set; }
        public string[] Allowed { get; set; }
        public UserLevel Lvl { get; set; }

        public string Token(int before = 0)
        {
            return Name + ".." + TimeToken(before) + ".." + HPass;
        }

        public string TimeToken(int before)
        {
            return Core.HashStr(Core.TempSecret(before) + HPass);
        }


        public bool Validate(string path)
        {
            if (Lvl == UserLevel.none) return false;
            if (Lvl == UserLevel.root) return true;
            if (path.Length == 0) return false;

            if (Lvl >= UserLevel.guest)
            {
                if (path.StartsWith(Core.CreateAirSharedDir()))
                {
                    return true;
                }

                bool valid = false;
                foreach (string p in Allowed)
                {
                    if (path.StartsWith(p))
                    {
                        valid = true;
                        break;
                    }
                }
                return valid;
            }

            if (Lvl == UserLevel.censored)
            {

            }

            return false;
        }
    }

    public enum UserLevel
    {
        none,
        guest,
        censored,
        root

    }
}