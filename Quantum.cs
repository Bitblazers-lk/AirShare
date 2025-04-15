using System;
using System.Timers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirShare
{
    public static class Quantum
    {
        public static Timer Tmr1min = new Timer(60000);
        public static Timer Tmr10min = new Timer(600000);
        public static Timer Tmr30min = new Timer(1800000);



        public static string getSandPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "airshareconfig", "sand");
        }

        public static void Init()
        {
            Tmr1min.Elapsed += Do1min;
            Tmr10min.Elapsed += Do10min;
            Tmr30min.Elapsed += Do30min;

            Tmr1min.Start();
            Tmr10min.Start();
            Tmr30min.Start();

            Directory.CreateDirectory(getSandPath());
            LogAlive();

        }

        private static void Do1min(object sender, ElapsedEventArgs e)
        {
            if (Core.UnixShell)
            {
                LetMeSleep();
            }

        }

        private static void LetMeSleep()
        {
            try
            {
                if (Settings.SystemControlSettings.LetMeSleep)
                {

                    bool Please = false;

                    DateTime dt = DateTime.Now;
                    if (Settings.SystemControlSettings.LastTime > dt)
                    {
                        Please = true;
                    }
                    else
                    {
                        Settings.SystemControlSettings.LastTime = dt;
                        Settings.SaveSystemControlSettings();
                    }

                    int H = DateTime.Now.Hour;
                    if (H >= Settings.SystemControlSettings.LetMeSleepStart1 && H <= Settings.SystemControlSettings.LetMeSleepEnd1)
                    {
                        Please = true;
                    }
                    else if (H >= Settings.SystemControlSettings.LetMeSleepStart2 && H <= Settings.SystemControlSettings.LetMeSleepEnd2)
                    {
                        Please = true;
                    }
                    if (Please)
                    {
                        ProgramMgr.Start("scripts/system-suspend.sh", ProgramIO.Default);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Core.LogErr(ex);
            }
        }





        private static async void Do10min(object sender, ElapsedEventArgs e)
        {
            LogAlive();

            try
            {
                await ProgramMgr.UpdateInternetServer();
            }
            catch (System.Exception ex)
            {
                Core.LogErr(ex);
            }


            if (Core.UnixShell)
            {
                if (Settings.SystemControlSettings.Monitor)
                {
                    try
                    {
                        ProgramMgr.Monitor(ProgramIO.Default);
                    }
                    catch (System.Exception ex)
                    {
                        Core.LogErr(ex);
                    }
                }


                if (Settings.SystemControlSettings.AutoUpdate)
                {
                    try
                    {
                        ProgramMgr.UpdateCodeBase(ProgramIO.Default);
                    }
                    catch (System.Exception ex)
                    {
                        Core.LogErr(ex);
                    }
                }
            }

        }

        private static void LogAlive()
        {
            try
            {
                File.AppendAllText(Path.Combine(getSandPath(), $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt"), $"\nAlive {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
            }
            catch (System.Exception ex)
            {
                Core.LogErr(ex);
            }
        }


        private static async void Do30min(object sender, ElapsedEventArgs e)
        {
            await Task.Delay(100);
        }



    }
}