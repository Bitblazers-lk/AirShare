using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AirShare.Data;

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

        public string IconicType
        {
            get
            {
                return FileTypeIconConverter.GetFileTypeIcon(Path.GetExtension(Name).TrimStart('.')).ToString();
            }
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
        Archive = 256,
        Presentation = 512,
        SpreadSheet = 1024,
        OfficeDocument = 2048,
        Other = 0b1000000000000000000000000000000
    }


    public class DirectoryEntries
    {

        public string Path { get; set; }

        public List<FSEntry> SubDirs { get; set; } = new List<FSEntry>();
        public List<FSEntry> Files { get; set; } = new List<FSEntry>();
        public DateTime LA { get; set; } //Last Access Time

    }
}
