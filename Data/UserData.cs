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

        public FSPermission Validate(string path)
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
        // public string[] Allowed { get; set; }
        public Dictionary<string, FSPermission> Allowed { get; set; }

        public UserLevel Lvl { get; set; }

        public string Token(int before = 0)
        {
            return Name + ".." + TimeToken(before) + ".." + HPass;
        }

        public string TimeToken(int before)
        {
            return Core.HashStr(Core.TempSecret(before) + HPass);
        }


        public bool Validate(string path, FSPermission P)
        {
            return ((Validate(path) & P) != 0);
        }

        public FSPermission Validate(string path)
        {
            if (path.Length == 0) return FSPermission.None;

            switch (Lvl)
            {
                case UserLevel.none:
                    return FSPermission.None;

                case UserLevel.root:
                    return FSPermission.Full;

                case UserLevel.guest:
                    if (path.StartsWith(Core.GetAirSharedDir()))
                    {
                        return FSPermission.Read;
                    }
                    else
                    {
                        return FSPermission.None;
                    }

                case UserLevel.censored:
                    {
                        if (path.StartsWith(Core.GetAirSharedDir()))
                        {
                            return FSPermission.Write;
                        }

                        foreach (var p in Allowed)
                        {
                            if (path.StartsWith(p.Key))
                            {
                                return p.Value;
                            }
                        }
                        return FSPermission.None;
                    }

                default:
                    return FSPermission.None;
            }



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