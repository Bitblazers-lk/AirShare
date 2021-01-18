using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace AirShare
{

    public static class HashLinks
    {
        private static Dictionary<string, HashLink> Links { get; set; } = new Dictionary<string, HashLink>();

        public static string AddFile(string Path, int Times)
        {
            HashLink H = new HashLink(Path, Times, HashLink.HashLinkType.File);
            string HS = Core.HashStr(Path) + Core.HashStr(DateTime.UtcNow.Ticks.ToString() + '-' + DateTime.Now.Millisecond.ToString());
            Links[HS] = H;
            return HS;
        }

        public static string AddDirectory(HashedDirectory HD)
        {
            HashLink H = new HashLink(Core.ToJSON(HD), 1, HashLink.HashLinkType.HashedDirectory);
            string HS = 'D' + Core.HashStr(HD.BasePath) + Core.HashStr(DateTime.UtcNow.Ticks.ToString() + '-' + DateTime.Now.Millisecond.ToString());
            Links[HS] = H;
            return HS;
        }



        public static HashLink GetLink(string HS)
        {
            if (Links.TryGetValue(HS, out HashLink H))
            {
                H.Times--;
                if (H.Times <= 0)
                {
                    Links.Remove(HS);
                }
                return H;
            }
            else
            {
                return null;
            }
        }


    }


    public class HashLink
    {
        public HashLink(string path, int times, HashLinkType T)
        {
            Data = path;
            Times = times;
            Type = T;
        }

        public string Data { get; set; }
        public int Times { get; set; }
        public HashLinkType Type { get; set; }

        public enum HashLinkType
        {
            File,
            HashedDirectory
        }

    }



    public class HashedDirectory
    {

        // Make sure to User.Validate(Path) before this 
        public HashedDirectory(string basePath, string baseURL, int times, string lang = "bash")
        {
            BaseURL = baseURL;
            Lang = lang;
            BasePath = basePath;
            Times = times;
        }

        public HashedDirectory()
        {
        }

        public string BasePath { get; set; }
        public string BaseURL { get; set; }

        public int Times { get; set; } = 1;


        public string Lang { get; set; }
        HashedDirectoryLanguage Language;



        private void SetLanguage()
        {
            switch (Lang)
            {
                case "bash":
                default:
                    Language = new HashedDirectoryLanguageBash();
                    break;

            }
        }

        public string GetExtention()
        {

            if (Language == null)
                SetLanguage();
            return Language.Extention;

        }

        public string GenerateString()
        {
            StringBuilder SB = new StringBuilder();

            foreach (string s in Enumerate())
            {
                SB.Append(s);
            }
            return SB.ToString();

        }

        // public string GenerateString()
        // {
        //     SetLanguage();
        //     SB = new StringBuilder();


        //     Rec(new DirectoryInfo(BasePath), Path.GetDirectoryName(BasePath) + '/');
        //     return SB.ToString();


        // }

        // private void Rec(DirectoryInfo DI, string Path)
        // {

        //     SB.Append(Language.CreateDir(Path));

        //     foreach (FileInfo fi in DI.EnumerateFiles())
        //     {
        //         string H = HashLinks.AddFile(fi.FullName, Times);
        //         SB.Append(Language.DownloadFile($"{BaseURL}hlnk/{fi.Name}?{H}", Path + fi.Name));
        //     }


        //     foreach (DirectoryInfo sd in DI.EnumerateDirectories())
        //     {
        //         Rec(sd, Path + sd.Name + "/");
        //     }
        // }




        public System.Collections.Generic.IEnumerable<string> Enumerate()
        {
            SetLanguage();

            yield return Language.Begin;

            foreach (var item in SubEnumerate(new DirectoryInfo(BasePath), Path.GetFileName(BasePath) + '/'))
            {
                yield return item;
            }
        }

        private System.Collections.Generic.IEnumerable<string> SubEnumerate(DirectoryInfo DI, string Path)
        {
            yield return Language.CreateDir(Path);

            foreach (FileInfo fi in DI.EnumerateFiles())
            {
                string H = HashLinks.AddFile(fi.FullName, Times);
                yield return (Language.DownloadFile($"{BaseURL}hlnk/{Core.ToURLPart(fi.Name)}?{H}", Path + fi.Name));
            }


            foreach (DirectoryInfo sd in DI.EnumerateDirectories())
            {
                foreach (var yr in SubEnumerate(sd, Path + sd.Name + "/"))
                {
                    yield return yr;
                }

            }
        }

    }
    public abstract class HashedDirectoryLanguage
    {
        public abstract string Extention { get; }
        public abstract string Begin { get; }
        public abstract string DownloadFile(string URL, string Path);
        public abstract string CreateDir(string Path);

    }

    // Only linux and Mac
    public class HashedDirectoryLanguageBash : HashedDirectoryLanguage
    {

        public override string Extention { get => ".sh"; }
        public override string Begin { get => "#!/bin/bash\n\n"; }
        public override string DownloadFile(string URL, string Path)
        {
            return $"wget -c -O \"{Path}\" {URL}\n";
        }
        public override string CreateDir(string Path)
        {
            return $"mkdir \"{Path}\"\n";
        }

    }


}