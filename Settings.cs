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
using Xabe.FFmpeg;

namespace AirShare
{
    public static class Settings
    {
        public static string[] Args;

        public static async void Init()
        {
            Core.ContentRootPath = Environment.CurrentDirectory;
            string cwd = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AirShare");
            Directory.CreateDirectory(cwd);
            Environment.CurrentDirectory = cwd;

            Core.Log($"Working on {cwd}");


            _ = SystemControlSettings;

            Core.Log($"OS version {Environment.OSVersion.VersionString}");
            if (OperatingSystem.IsLinux())
            {
                Core.Log("Linux Commands supported");
                Core.UnixShell = true;
            }
            // if (!Directory.Exists(Environment.CurrentDirectory + "Tmp"))
            //     Directory.CreateDirectory(Environment.CurrentDirectory + "Tmp");

            if (Args != null && Args.Length > 0)
            {
                ParseArgs();
            }
            else
            {
                Core.Log("No Args");
            }


            FFmpeg.SetExecutablesPath(Environment.CurrentDirectory);
            if (Settings.SystemControlSettings.PublicServer)
            {
                ProgramMgr.StartNGROK(ProgramIO.Default);
                await Task.Delay(3000);
                await ProgramMgr.UpdateInternetServer();
            }

        }

        public static void ParseArgs()
        {
            foreach (string arg in Args)
            {
                Core.Log("arg : " + arg);
                switch (arg)
                {
                    case "+ps":
                        Settings.SystemControlSettings.PublicServer = true;
                        break;

                    case "-ps":
                        Settings.SystemControlSettings.PublicServer = false;
                        break;

                    case "+bs":
                        Settings.SystemControlSettings.BroadcastPublicServer = true;
                        break;

                    case "-bs":
                        Settings.SystemControlSettings.BroadcastPublicServer = false;
                        break;

                    case "+m":
                        Settings.SystemControlSettings.Monitor = true;
                        break;

                    case "-m":
                        Settings.SystemControlSettings.Monitor = false;
                        break;

                    case "+h":
                        Settings.SystemControlSettings.Hidden = true;
                        Core.ConfigureVisibility();
                        break;

                    case "-h":
                        Settings.SystemControlSettings.Hidden = false;
                        Core.ConfigureVisibility();
                        break;

                    case "-ngauth":
                        ProgramMgr.DeauthNGROK(ProgramIO.Default);
                        break;

                    case "+as":
                        ProgramMgr.ConfigAutoStart(ProgramIO.Default);
                        break;

                    case "-as":
                        ProgramMgr.DisableAutoStart(ProgramIO.Default);
                        break;

                    case "+au":
                        Settings.SystemControlSettings.AutoUpdate = true;
                        break;

                    case "-au":
                        Settings.SystemControlSettings.AutoUpdate = false;
                        break;

                    default:
                        if (arg.StartsWith("ngauth="))
                        {
                            ProgramMgr.AuthNGROK(ProgramIO.Default, arg.Substring("ngauth=".Length).Trim());
                        }
                        else
                        {
                            Core.Log($"Cannot parse arg : {arg}");
                        }
                        break;
                }

            }
            SaveSystemControlSettings();

        }


        private static string getSystemControlSettingsPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "airshareconfig", "sysctrl.json");
        }

        private static SystemControlSettings systemControlSettings;
        public static SystemControlSettings SystemControlSettings
        {
            get
            {
                if (systemControlSettings == null)
                {
                    LoadFile(ref systemControlSettings, getSystemControlSettingsPath());
                }
                return systemControlSettings;
            }
            set
            {
                systemControlSettings = value;

            }
        }
        public static void SaveSystemControlSettings()
        {
            SaveFile(SystemControlSettings, getSystemControlSettingsPath());

        }

        public static void LoadFile<T>(ref T S, string path, bool Force = false) where T : IMiniDataFile
        {
            if (Force || S == null)
            {
                if (!File.Exists(path))
                {
                    S = Activator.CreateInstance<T>();
                    S.CreateNew();
                    SaveFile(S, path);
                    Core.Log($"Created new Data File {path} ");

                }
                else
                {
                    string sA = File.ReadAllText(path);
                    try
                    {
                        S = Core.FromJSON<T>(sA);
                    }
                    catch (System.Exception)
                    {
                        S = Activator.CreateInstance<T>();
                        S.CreateNew();
                        SaveFile(S, path);
                        Core.Log($"Recreated corrupted Data File {path} ");
                    }

                }
            }
        }

        public static void SaveFile<T>(T S, string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            File.WriteAllText(path, Core.ToJSON(S));
        }


    }


    public interface IMiniDataFile
    {
        public void CreateNew();
    }
    public class SystemControlSettings : IMiniDataFile
    {
        public bool LetMeSleep { get; set; }
        public int LetMeSleepStart1 { get; set; }
        public int LetMeSleepEnd1 { get; set; }
        public int LetMeSleepStart2 { get; set; }
        public int LetMeSleepEnd2 { get; set; }

        public DateTime LastTime { get; set; }
        public bool AutoUpdate { get; set; }
        public bool PublicServer { get; set; }
        public string PublicServerInfo { get; set; }
        public bool BroadcastPublicServer { get; set; }
        public bool Monitor { get; set; }
        public bool Hidden { get; set; }
        // public string PublicServerLog = "";

        public void CreateNew()
        {
            LetMeSleep = false;

            LetMeSleepStart1 = 23;
            LetMeSleepEnd1 = 24;

            LetMeSleepStart2 = 0;
            LetMeSleepEnd2 = 6;

            LastTime = DateTime.Now;
            AutoUpdate = false;

            PublicServer = false;

            //This wont work unless PublicServer is true
            BroadcastPublicServer = true;

            Hidden = false;

            Monitor = false;

        }


    }
}