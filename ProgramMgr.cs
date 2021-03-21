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

        public static async Task MakePublicServer()
        {
            if (Settings.SystemControlSettings.PublicServer)
            {

                Settings.SystemControlSettings.PublicServerLog = "";
                Process pr = Start("scripts/localhostrun.sh", Settings.SystemControlSettings.GetPublicServerLog());



                if (pr == null)
                {
                    return;
                }
                else
                {
                    if (Settings.SystemControlSettings.BroadcastPublicServer)
                    {
                        await Task.Delay(10000);
                        var lines = Settings.SystemControlSettings.PublicServerLog.Split(Environment.NewLine, 100);
                        string shout = "";
                        foreach (string item in lines)
                        {
                            if (item.Contains("tunneled"))
                            {
                                shout += item + Environment.NewLine;
                            }
                        }
                        string jshout = Core.ToCompactJSON(shout);


                        HttpClient httpClient = new HttpClient();
                        await httpClient.GetStringAsync("https://airshare.requestcatcher.com/public?" + jshout);
                    }
                }
            }

        }
    }
}