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
using System.Diagnostics;
using System.Net.Http;

namespace AirShare
{
    public class ProgramIO
    {
        public Action<string> OutputRec;
        public Action<string> ErrorRec;
        public string Args = "";


        public static ProgramIO Default = new ProgramIO()
        {
            OutputRec = Core.Log,
            ErrorRec = (string s) => Core.Log("PIO Error " + s)
        };
    }


    public static class ProgramMgr
    {
        public static Process Start(string name, ProgramIO PIO, bool UserDir = false)
        {
            Process process = new Process();

            process.StartInfo.FileName = Path.Combine(Core.ContentRootPath, name);
            if (UserDir)
            {
                process.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            }
            else
            {
                process.StartInfo.WorkingDirectory = Directory.GetParent(process.StartInfo.FileName).FullName;
            }

            process.StartInfo.Arguments = PIO.Args;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += (sender, data) => PIO.OutputRec(data.Data);
            process.ErrorDataReceived += (sender, data) => PIO.ErrorRec(data.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return process;
        }

        public static bool RunSample(ProgramIO PIO)
        {
            Process pr = Start("scripts/sample-prog.sh", PIO);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool UpdateCodeBase(ProgramIO PIO)
        {

            Process pr = Start("scripts/git-update.sh", PIO);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool Monitor(ProgramIO PIO)
        {

            Process pr = Start("scripts/monitor.sh", PIO, true);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ConfigAutoStart(ProgramIO PIO)
        {

            Process pr = Start("scripts/config-auto-start.sh", PIO);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool DisableAutoStart(ProgramIO PIO)
        {

            Process pr = Start("scripts/disable-auto-start.sh", PIO);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        public static async Task<string> UpdateInternetServer(bool Retry = true)
        {
            if (Settings.SystemControlSettings.PublicServer)
            {
                Settings.SystemControlSettings.PublicServerInfo = "Changing...";

                HttpClient httpClient = new HttpClient();

                string s;
                try
                {
                    s = await httpClient.GetStringAsync("http://127.0.0.1:36129/api/tunnels");
                }
                catch (HttpRequestException he)
                {
                    Core.LogErr(he);

                    if (Retry)
                    {
                        StartNGROK(ProgramIO.Default);
                        return await UpdateInternetServer(false);
                    }

                    Settings.SystemControlSettings.PublicServerInfo = $"{Core.ErrorMsg(he)}";
                    return Settings.SystemControlSettings.PublicServerInfo;
                }
                catch (System.Exception ex)
                {
                    Core.LogErr(ex);
                    Settings.SystemControlSettings.PublicServerInfo = $"{Core.ErrorMsg(ex)}";
                    return Settings.SystemControlSettings.PublicServerInfo;
                }


                string svrinfo;

                List<string> URLs = Core.ParseBetween(s, "public_url", ",");
                if (URLs.Count == 0)
                {

                    if (Retry)
                    {
                        StartNGROK(ProgramIO.Default);
                        return await UpdateInternetServer(false);
                    }
                    svrinfo = s.Substring(0, Math.Min(s.Length, 2048));

                }
                else
                {
                    svrinfo = "URLs : \n" + string.Join('\n', URLs);
                }

                Core.Log("\n:_Public_Server_:\n" + svrinfo + "\n:_END_Public_Server_:\n");

                Settings.SystemControlSettings.PublicServerInfo = svrinfo;

                if (Settings.SystemControlSettings.BroadcastPublicServer)
                {
                    string brd = $"{Environment.UserName}@{Environment.UserDomainName} - {Environment.MachineName} ( {Environment.OSVersion.VersionString} ) \n{Settings.SystemControlSettings.PublicServerInfo}";
                    await httpClient.PostAsync("https://airshare.requestcatcher.com/public",
                     new System.Net.Http.StringContent(brd));
                }

                return Settings.SystemControlSettings.PublicServerInfo;

            }

            Settings.SystemControlSettings.PublicServerInfo = null;
            return "";
        }

        public static bool StartNGROK(ProgramIO PIO)
        {

            Process pr = Start("scripts/tunnel-start.sh", PIO, true);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool SetupNGROK(ProgramIO PIO)
        {

            Process pr = Start("scripts/ngrok-setup.sh", PIO, true);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool DeauthNGROK(ProgramIO PIO)
        {
            File.WriteAllText("sand/ngrok/auth.yml", "");
            StartNGROK(PIO);
            return true;
        }

        public static bool AuthNGROK(ProgramIO PIO, string t)
        {
            File.WriteAllText("sand/ngrok/auth.yml", $"authtoken: {t}");
            StartNGROK(PIO);
            return true;
        }




    }
}