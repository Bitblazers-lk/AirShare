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


        public static void Init()
        {
            Tmr1min.Elapsed += Do1min;
            Tmr10min.Elapsed += Do10min;
            Tmr30min.Elapsed += Do30min;

            Tmr1min.Start();
            Tmr10min.Start();
            Tmr30min.Start();
        }

        private static void Do1min(object sender, ElapsedEventArgs e)
        {
            
        }

        private static void Do10min(object sender, ElapsedEventArgs e)
        {

        }
        private static void Do30min(object sender, ElapsedEventArgs e)
        {

        }



    }
}