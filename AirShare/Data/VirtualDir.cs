using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirShare
{
    public class VirtualDir
    {

        public string VirtualPath { get; set; }
        public Dictionary<string,string> SubDirs { get; set; } = new Dictionary<string, string>();
        public Dictionary<string,string> Files { get; set; } = new Dictionary<string, string>();

    }


    public class VirtualDirs
    {
        public Dictionary<string, VirtualDir> Dirs { get; set; } = new Dictionary<string, VirtualDir>();
    }

}