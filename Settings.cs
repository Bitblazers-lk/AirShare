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
        public static async void Init()
        {
            _ = SystemControlSettings;

            Core.Log($"OS version { Environment.OSVersion.VersionString}");
            if (OperatingSystem.IsLinux())
            {
                Core.Log("Linux Commands supported");
                Core.UnixShell = true;
            }
            if (!Directory.Exists(Environment.CurrentDirectory+"Tmp"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "Tmp");

            FFmpeg.SetExecutablesPath(Environment.CurrentDirectory);
            await ProgramMgr.MakePublicServer();

        }


        private static string Path_systemControlSettings = "sysctrl.json";
        private static SystemControlSettings systemControlSettings;
        public static SystemControlSettings SystemControlSettings
        {
            get
            {
                if (systemControlSettings == null)
                {
                    LoadFile(ref systemControlSettings, Path_systemControlSettings);
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
            SaveFile(SystemControlSettings, Path_systemControlSettings);

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
        public bool BroadcastPublicServer { get; set; }
        public string PublicServerLog = "";

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

        }

        public ProgramIO GetPublicServerLog()
        {
            return new ProgramIO()
            {
                OutputRec = (string s) => { PublicServerLog = s + Environment.NewLine + PublicServerLog; },
                ErrorRec = (string s) => { PublicServerLog = "Error : " + s + Environment.NewLine + PublicServerLog; }
            };
        }
    }
}