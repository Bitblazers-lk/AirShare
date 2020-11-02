using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirShare
{
    public class FSEntry
    {
        public string Name { get; set; }
        public FSFileAttrib Atrb { get; set; }


        public bool CheckIs(FSFileAttrib chk)
        {
            return (Atrb & chk) != 0;
        }

    }

    [Flags]
    public enum FSFileAttrib
    {
        Directory = 0,
        File = 1,
        Readonly = 2,

        Text = 4,
        Excecutable = 8,
        Video = 16,
        Audio = 32,
        Image = 64,
        Document = 128,
        Other = 4096


    }


    public class DirectoryEntries
    {

        public string Path { get; set; }

        public List<FSEntry> SubDirs { get; set; } = new List<FSEntry>();
        public List<FSEntry> Files { get; set; } = new List<FSEntry>();
        public DateTime LA { get; set; } //Last Access Time

    }
}
