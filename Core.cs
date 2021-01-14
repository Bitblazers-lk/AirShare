using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Json.Serialization;

using System.Collections.Generic;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Diagnostics.CodeAnalysis;

namespace AirShare
{
    public static class Core
    {
        public static MessageBoxClass MsgBox;
        public static bool UnixShell;
        public static JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        public static string ToJSON<T>(T Obj)
        {
            return JsonSerializer.Serialize(Obj, JsonOptions);
        }
        public static T FromJSON<T>(string JSON)
        {
            return JsonSerializer.Deserialize<T>(JSON, JsonOptions);
        }

        public static void Log(string S)
        {
            System.Console.WriteLine($" >> {S}");
        }

        public static void LogErr(System.Exception ex)
        {
            System.Console.WriteLine(ErrorMsg(ex));
        }

        public static string ErrorMsg(Exception ex)
        {
            return $" !> {ex} {ex.Message} \n Stack : \n{ex.StackTrace} \n \n Inner [{ex.InnerException} {ex.InnerException?.Message} {ex.InnerException?.StackTrace} \n]";
        }


        private static AuthDetails AD;

        public static AuthDetails Authdetails
        {
            get
            {
                LoadAD();
                return AD;
            }
        }


        public static void LoadAD()
        {
            if (AD == null)
            {
                if (!File.Exists("auth.json"))
                {
                    CreateDefaultAD();
                    SaveAD();
                }
                else
                {
                    string sA = File.ReadAllText("auth.json");
                    try
                    {
                        AD = FromJSON<AuthDetails>(sA);
                    }
                    catch (System.Exception)
                    {
                        CreateDefaultAD();
                        SaveAD();

                        Log("Password reset");
                    }

                }
            }
        }


        public static User Auth(string name, string pass)
        {
            return AuthHashed(name, HashStr(pass));
        }

        private static User AuthHashed(string name, string pass)
        {

            LoadAD();
            if (name == null) return null;
            if (AD.Pars.TryGetValue(name, out User usr))
            {
                if (usr.HPass == pass)
                {
                    UserTokens[usr.Token()] = usr;
                    return usr;
                }
            }

            if (GuestUsers.TryGetValue(name, out User gu))
            {
                if (gu.HPass == pass)
                {
                    UserTokens[gu.Token()] = gu;
                    return gu;
                }
            }

            return null;
        }

        static string AirSharedDir;
        public static string GetAirSharedDir()
        {
            if (AirSharedDir == null)
            {
                string ASD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "AirShared");
                try
                {

                    AirSharedDir = Directory.CreateDirectory(ASD).FullName;
                    File.WriteAllText(Path.Combine(AirSharedDir, "GuestShared.txt"), "This directory is shared with guests. No passwords required");
                }
                catch (System.Exception)
                {
                    AirSharedDir = Directory.CreateDirectory(ASD + TempSecret()).FullName;
                    File.WriteAllText(Path.Combine(AirSharedDir, "GuestShared.txt"), "This directory is shared with guests. No passwords required");

                }

                Log($"AirShared : {AirSharedDir} \t CurrentDir : {Environment.CurrentDirectory}");
            }
            return AirSharedDir;
        }




        static readonly Dictionary<string, User> UserTokens = new Dictionary<string, User>();
        static readonly Dictionary<string, User> GuestUsers = new Dictionary<string, User>();

        public static User AuthToken(string t)
        {
            if (t == null) return null;
            if (UserTokens.TryGetValue(t, out User usr))
            {
                return usr;
            }
            else
            {
                string[] ts = Uri.UnescapeDataString(t).Split("..", 3);
                if (ts.Count() == 3)
                {
                    usr = AuthHashed(ts[0], ts[2]);
                    if (usr != null)
                    {

                        bool SOK = false;
                        for (int i = 0; i < 3; i++)
                        {
                            if (ts[1] == usr.TimeToken(i))
                            {
                                SOK = true;
                                break;
                            }
                        }


                        if (SOK)
                        {
                            Log($"Core.AuthToken : Token auth succes {t}");
                            return usr;
                        }
                        else
                        {
                            Log($"Core.AuthToken : Token auth Expired {t}");
                        }
                    }
                    else
                    {
                        Log($"Core.AuthToken : Token auth Failed {t}");
                    }
                }
            }

            return null;
        }

        public static User AuthReq(Microsoft.AspNetCore.Http.HttpRequest Req, Microsoft.AspNetCore.Http.HttpResponse Resp)
        {
            if (Req.Cookies.TryGetValue("ut", out string token))
            {
                User usr = AuthToken(token);
                if (usr != null)
                {
                    Resp.Cookies.Append("ut", usr.Token());
                    return usr;
                }
            }
            return null;
        }



        public static IEnumerable<User> GetUsers()
        {
            LoadAD();
            return AD.Pars.Values;
        }

        public static void CreateDefaultAD()
        {
            AD = new AuthDetails()
            {
                Pars = new Dictionary<string, User>()
                {
                    { "admin", new User()
                        {Name = "admin", Allowed = new Dictionary<string, FSPermission>(){ {"/", FSPermission.Full}, {"\\", FSPermission.Read}}, Lvl = UserLevel.root, HPass = HashStr("pass") }
                    }
                }
            };

        }
        public static void SaveAD()
        {
            File.WriteAllText("auth.json", ToJSON(AD));
        }

        public static bool ChangePass(string Name, string OldPass, string newPass)
        {
            LoadAD();
            User usr = Auth(Name, OldPass);
            if (usr == null)
            {
                return false;
            }

            usr.HPass = HashStr(newPass);
            SaveAD();

            return true;

        }

        public static User AddUser(string Name, string newPass, UserLevel lvl, string allowedR, string allowedRW, string allowedRWX)
        {
            LoadAD();

            bool useR = true;
            allowedR = allowedR.Trim();
            if (string.IsNullOrWhiteSpace(allowedR))
            {
                useR = false;
            }

            bool useRW = true;
            allowedRW = allowedRW.Trim();
            if (string.IsNullOrWhiteSpace(allowedRW))
            {
                useRW = false;
            }

            bool useRWX = true;
            allowedRWX = allowedRWX.Trim();
            if (string.IsNullOrWhiteSpace(allowedRWX))
            {
                useRWX = false;
            }
            string[] allR = allowedR.Split(Environment.NewLine);
            // Array.Sort(allR, new CompareByLength());

            string[] allRW = allowedRW.Split(Environment.NewLine);
            // Array.Sort(allRW, new CompareByLength());

            string[] allRWX = allowedRWX.Split(Environment.NewLine);
            // Array.Sort(allRWX, new CompareByLength());

            List<string> allL = new List<string>();
            if (useR) allL.AddRange(allR);
            if (useRW) allL.AddRange(allRW);
            if (useRWX) allL.AddRange(allRWX);

            string[] all = allL.ToArray();

            Array.Sort(all, new CompareByLength());

            Dictionary<string, FSPermission> Allowed = new Dictionary<string, FSPermission>();

            foreach (string item in all)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    Allowed[item] = FSPermission.None;
            }

            if (useR)
                foreach (string p in allR)
                {
                    if (!string.IsNullOrWhiteSpace(p))
                        Allowed[p] = FSPermission.Read;
                }

            if (useRW)
                foreach (string p in allRW)
                {
                    if (!string.IsNullOrWhiteSpace(p))
                        Allowed[p] = FSPermission.Write;
                }
            if (useRWX)
                foreach (string p in allRWX)
                {
                    if (!string.IsNullOrWhiteSpace(p))
                        Allowed[p] = FSPermission.Full;
                }


            User usr = new User
            {
                Name = Name,
                Lvl = lvl,
                HPass = HashStr(newPass),
                Allowed = Allowed
            };

            AD.Pars[usr.Name] = usr;
            SaveAD();

            return usr;

        }



        public static bool RemoveUser(string Name)
        {

            LoadAD();
            bool r = AD.Pars.Remove(Name);
            SaveAD();
            return r;
        }



        public static User AddGuest()
        {
            User g = new User
            {
                Name = "Guest-" + DateTime.UtcNow.Ticks.ToString(),
                Lvl = UserLevel.guest,
                HPass = HashStr(DateTime.UtcNow.Ticks.ToString() + DateTime.Now.Millisecond.ToString()),
                Allowed = new Dictionary<string, FSPermission>()
            };

            GuestUsers[g.Name] = g;

            return g;

        }





        static byte[] salt = null;
        public static string HashStr(string s)
        {
            if (salt == null)
            {
                salt = new byte[128 / 8];
                for (int i = 0; i < (128 / 8); i++)
                {
                    salt[i] = (byte)(((i * i) + 128 - i) | 255);
                }
            }


            byte[] keys = (KeyDerivation.Pbkdf2(
            password: s,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 128 / 8));

            return BitConverter.ToString(keys);

        }

        public static string TempSecret(int before = 0)
        {
            return DateTime.UtcNow.Date.AddMinutes((DateTime.UtcNow.Hour * 60) + ((DateTime.UtcNow.Minute / 30) * 30) - (before * 30)).Ticks.ToString();
        }


        public static T LoadSetting<T>(string filepath)
        {
            T DB;

            if (!File.Exists(filepath))
            {
                DB = Activator.CreateInstance<T>();
                SaveSetting<T>(DB, filepath);
            }
            else
            {
                string sA = File.ReadAllText(filepath);
                try
                {
                    DB = FromJSON<T>(sA);
                }
                catch (System.Exception ex)
                {
                    DB = Activator.CreateInstance<T>();
                    SaveSetting<T>(DB, filepath);

                    LogErr(ex);
                    Log("Setting corrupt .`. Reset " + filepath);
                }

            }

            return DB;
        }


        public static void SaveSetting<T>(T DB, string filepath)
        {
            try
            {
                File.WriteAllText(filepath, ToJSON(DB));
            }
            catch (System.Exception ex)
            {
                LogErr(ex);
            }

        }


        public static bool IsVirtualDir(string path)
        {
            return path.StartsWith("vdir:", StringComparison.CurrentCultureIgnoreCase);
        }


        private static VirtualDirs vDirs;
        public static VirtualDirs VDirs
        {
            get
            {
                if (vDirs == null)
                {

                    GetAirSharedDir();
                    vDirs = LoadSetting<VirtualDirs>("vdirs.json");
                    // if (vDirs.Dirs.Count == 0)
                    // {

                    // }
                    vDirs.Dirs["-/AirShared"] = new VirtualDir()
                    {
                        VirtualPath = "-/AirShared",
                        SubDirs = new Dictionary<string, string>() { { "AirShared", "AirShared" } }
                    };
                }

                return vDirs;

            }

            set
            {
                vDirs = value;
                SaveSetting(vDirs, "vdirs.json");
            }
        }



    }

    public class CompareByLength : IComparer<string>
    {
        public int Compare([AllowNull] string x, [AllowNull] string y)
        {
            return (x.Length - y.Length);
        }
    }

}
