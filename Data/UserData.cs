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
        public bool IsDefault { get; set; } = true;
        public Dictionary<string, User> Pars { get; set; } = new Dictionary<string, User>();
    }
    public class User
    {
        public string Name { get; set; }
        public string HPass { get; set; }
        // public string[] Allowed { get; set; }
        public Dictionary<string, FSPermission> Allowed { get; set; }

        public UserLevel Lvl { get; set; }


        [NonSerialized]
        private DirectoryEntries defaultDEs;
        public DirectoryEntries DefaultDEs()
        {
            if (defaultDEs == null)
            {
                defaultDEs = new DirectoryEntries();
                defaultDEs.Path = "-/";
                foreach (var item in Allowed)
                {
                    defaultDEs.SubDirs.Add(new FSEntry() { Name = item.Key, Atrb = FSFileAttrib.Directory });
                }


                foreach (string item in new List<string>()
                {
                    Core.GetAirSharedDir(),
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                    Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                })
                {
                    if (Validate(item, FSPermission.Read))
                    {
                        defaultDEs.SubDirs.Add(new FSEntry() { Name = item, Atrb = FSFileAttrib.Directory });
                    }
                }
            }
            return defaultDEs;
        }

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
                    else if (path.StartsWith("-/"))
                    {
                        return FSPermission.Read;
                    }
                    else
                    {
                        return FSPermission.None;
                    }

                case UserLevel.friend:
                    {
                        if (path.StartsWith(Environment.CurrentDirectory))
                        {
                            return FSPermission.None;
                        }
                        else if (path.StartsWith(Core.GetAirSharedDir()))
                        {
                            return FSPermission.Write;
                        }
                        else if (path.StartsWith("-/"))
                        {
                            return FSPermission.Read;
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
        friend,
        root
    }
}