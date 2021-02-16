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

        public string IconicTypeCSS
        {
            get
            {
                string ext = Path.GetExtension(Name);
                if (string.IsNullOrEmpty(ext))
                {
                    return "i-Other";
                }
                string ie = $"i-{ext[1..]}";
                if (Atrb == FSFileAttrib.Other)
                {
                    return ie;
                }

                string ia = MostSignificantAttrib.ToString();

                return $"i-{ia} {ie}";


            }
        }

        public FSFileAttrib MostSignificantAttrib
        {
            get
            {
                int a = (int)Atrb;
                int chk = 1 << 30;
                for (int i = 30; i > 1; i--)
                {
                    if ((a & chk) != 0)
                    {
                        return (FSFileAttrib)chk;
                    }
                    chk >>= 1;
                }

                return (a & 1) == 1 ? FSFileAttrib.Other : FSFileAttrib.Directory;
            }
        }

        public IEnumerable<FSFileAttrib> ListAttribs
        {
            get
            {
                int a = (int)Atrb;
                int chk = 1 << 30;
                for (int i = 30; i > -1; i--)
                {
                    if ((a & chk) != 0)
                    {
                        yield return (FSFileAttrib)chk;
                    }
                    chk >>= 1;
                }

                yield break;
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
        SourceCode = 8,
        Video = 16,
        Audio = 32,
        Image = 64,
        Document = 128,
        Archive = 256,
        Presentation = 512,
        SpreadSheet = 1024,
        OfficeDocument = 2048,
        Excecutable = 4096,
        Other = 0b1000000000000000000000000000000
    }

    [Flags]
    public enum FSPermission
    {
        None = 0,
        Read = 1,
        Write = 2 | Read,
        Excecute = 4 | Write,
        Full = 8 | Excecute
    }

    public class DirectoryEntries
    {

        public string Path { get; set; }

        public List<FSEntry> SubDirs { get; set; } = new List<FSEntry>();
        public List<FSEntry> Files { get; set; } = new List<FSEntry>();
        public DateTime LA { get; set; } //Last Access Time

    }
}
