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

namespace AirShare
{
    public class ProgramIO
    {
        public Action<string> OutputRec;
        public Action<string> ErrorRec;
        public string Args = "";
    }


    public static class ProgramMgr
    {
        public static Process Start(string name, ProgramIO PIO)
        {
            Process process = new Process();

            process.StartInfo.FileName = name;
            process.StartInfo.Arguments = PIO.Args;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.OutputDataReceived += (sender, data) => PIO.OutputRec(data.Data);
            process.ErrorDataReceived += (sender, data) => PIO.ErrorRec(data.Data);
            Console.WriteLine("starting");
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            return process;
        }

        public static bool RunSample(ProgramIO PIO)
        {
            Process pr = Start("sample-prog.sh", PIO);
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
            Process pr = Start("git-update.sh", PIO);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
         public static bool RestartApp(ProgramIO PIO)
        {
            Process pr = Start("Restart.sh", PIO);
            if (pr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}