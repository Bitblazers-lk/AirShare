using System;
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
        public static DateTime Last10min = DateTime.MinValue;
        public static DateTime Last1h = DateTime.MinValue;
        public static DateTime Last24h = DateTime.MinValue;



    }
}