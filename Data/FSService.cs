using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AirShare
{
    public static class FSService
    {

        private static DirectoryEntries HomeDirEntries;

        public static DirectoryEntries ListAllFiles(string path, User usr)
        {
            DirectoryEntries DE = new DirectoryEntries() { Path = path, LA = DateTime.Now };
            foreach (var item in ListFiles(path, usr))
            {
                DE = item;
            }
            return DE;
        }

        public static IEnumerable<DirectoryEntries> ListFiles(string path, User usr)
        {
            if (path == "/\\")
            {
                if (HomeDirEntries == null)
                {
                    HomeDirEntries = new DirectoryEntries() { Path = "", LA = DateTime.Now };

                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = "/--", Atrb = FSFileAttrib.Directory });

                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = Core.GetAirSharedDir(), Atrb = FSFileAttrib.Directory });
                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = Environment.GetFolderPath(Environment.SpecialFolder.Personal), Atrb = FSFileAttrib.Directory });
                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Atrb = FSFileAttrib.Directory });
                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), Atrb = FSFileAttrib.Directory });
                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), Atrb = FSFileAttrib.Directory });
                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), Atrb = FSFileAttrib.Directory });
                    HomeDirEntries.SubDirs.Add(new FSEntry() { Name = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Atrb = FSFileAttrib.Directory });

                    foreach (string item in Directory.GetLogicalDrives())
                    {
                        HomeDirEntries.SubDirs.Add(new FSEntry() { Name = item, Atrb = FSFileAttrib.Directory });
                    }
                }
                yield return HomeDirEntries;
                yield break;


            }
            else if (path.StartsWith("/--"))
            {
                if (path == "/--")
                {
                    yield return usr.DefaultDEs();
                    yield break;
                }
                else
                {
                    foreach (var item in ListFiles(path.Substring(2), usr))
                    {
                        yield return item;
                    }
                    yield break;
                }
            }
            else if (path.StartsWith('?'))
            {
                string[] QA = path.TrimStart('?').Split('?', 2);
                if (QA.Length == 2)
                {
                    foreach (var item in ListFilesQ(QA[1], QA[0]))
                    {
                        yield return item;
                    }

                    yield break;
                }
                else
                {
                    yield return ListFilesAbs(path);
                    yield break;
                }
            }
            else
            {
                yield return ListFilesAbs(path);
                yield break;

            }
        }

        // public static string ResolveVPath(string path)
        // {
        //     foreach (var vp in Core.VDirs.Dirs)
        //     {
        //         if (path.StartsWith(vp.Key))
        //         {

        //             foreach (var item in vp.Value.SubDirs)
        //             {
        //                 //May bug - Should Find all
        //                 if (path.StartsWith(item.Key))
        //                 {
        //                     var suf = path[item.Key.Length..];
        //                     var pp = System.IO.Path.Combine(item.Value, suf);

        //                     return pp;

        //                 }
        //             }
        //             foreach (var item in vp.Value.Files)
        //             {

        //             }
        //         }
        //     }
        // }

        public static DirectoryEntries ListFilesAbs(string path)
        {

            DirectoryInfo DI;
            try
            {
                DI = new DirectoryInfo(path);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ListFiles error " + path + " >> " + ex.Message);
                return new DirectoryEntries();
            }

            DirectoryEntries D = new DirectoryEntries();


            foreach (var item in DI.GetDirectories())
            {
                D.SubDirs.Add(new FSEntry()
                {
                    Name = item.Name,
                    Atrb = FSFileAttrib.Directory
                });
            }

            foreach (var item in DI.GetFiles())
            {

                D.Files.Add(new FSEntry()
                {
                    Name = item.Name,
                    Atrb = FSFileAttrib.File | FileType(item.Extension)
                });
            }
            //(item.IsReadOnly ? FSFileAttrib.Readonly : 0)

            D.Files.Sort(new FSEComparer());
            D.SubDirs.Sort(new FSEComparer());

            D.LA = DI.LastAccessTime;
            D.Path = DI.FullName;

            Core.Log($"cd {path}");

            return D;
        }

        public static IEnumerable<DirectoryEntries> ListFilesQ(string path, string Q)
        {

            if (string.IsNullOrEmpty(Q))
            {
                yield return ListFilesAbs(path);
                yield break;
            }



            DirectoryEntries D = new DirectoryEntries();
            string[] QA = Q.Split('/', 3);
            if (QA.Length == 0)
            {
                yield return ListFilesAbs(path);
                yield break;
            }

            string match = QA[QA.Length - 1];

            if (string.IsNullOrEmpty(match))
            {
                yield return ListFilesAbs(path);
                yield break;
            }
            match = match.ToLower();

            int QCount = 100;
            bool QDirs = true;
            bool QFiles = true;
            for (int i = 0; i < QA.Length - 1; i++)
            {
                string qa = QA[i].ToLower();
                if (int.TryParse(qa, out int qcount))
                {
                    QCount = qcount;
                }
                else if (qa.Contains('f'))
                {
                    QFiles = false;
                }
                else if (qa.Contains('d'))
                {
                    QDirs = false;
                }
            }


            DirectoryInfo DI;
            try
            {
                DI = new DirectoryInfo(path);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ListFiles error " + path + " >> " + ex.Message);
                yield break;
            }

            D.LA = DI.LastAccessTime;
            D.Path = $"?{Q}?{DI.FullName}";

            Core.Log($"cd {path}");

            int Count = 0;

            Queue<DirectoryInfo> DIs = new Queue<DirectoryInfo>();
            DIs.Enqueue(DI);

            while (DIs.TryDequeue(out DirectoryInfo di))
            {

                foreach (var item in GetFilesSafe(di))
                {
                    //TODO: Regex
                    if (QFiles && item.Name.ToLower().Contains(match))
                    {
                        Count++;

                        D.Files.Add(new FSEntry()
                        {
                            Name = item.FullName,
                            Atrb = FSFileAttrib.File | FileType(item.Extension)
                        });
                    }
                }


                if (QCount <= Count)
                {
                    yield return D;

                    Core.Log($"Search {path} for {Q}. Found {Count}. Max Results.");
                    yield break;
                }


                foreach (var item in GetDirectoriesSafe(di))
                {
                    //TODO: Regex
                    if (QDirs && item.Name.ToLower().Contains(match))
                    {
                        Count++;

                        D.SubDirs.Add(new FSEntry()
                        {
                            Name = item.FullName,
                            Atrb = FSFileAttrib.Directory
                        });
                    }
                    DIs.Enqueue(item);
                }



                if (QCount <= Count)
                {
                    yield return D;

                    Core.Log($"Search {path} for {Q}. Found {Count}. Max Results.");
                    yield break;
                }
                else
                {
                    yield return D;
                    Core.Log($"Search {path} for {Q}. Found {Count}. Searching more...");

                }
            }



            yield return D;

            Core.Log($"Search {path} for {Q}. Found {Count}. Search End.");
            yield break;
        }

        static DirectoryInfo[] GetDirectoriesSafe(DirectoryInfo di)
        {
            try
            {
                return di.GetDirectories();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ListFiles GetDirectoriesSafe error " + di.FullName + " >> " + ex.Message);
            }
            return new DirectoryInfo[0];
        }
        static FileInfo[] GetFilesSafe(DirectoryInfo di)
        {
            try
            {
                return di.GetFiles();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("ListFiles GetFilesSafe error " + di.FullName + " >> " + ex.Message);
            }
            return new FileInfo[0];
        }






        public class FSEComparer : IComparer<FSEntry>
        {
            public int Compare(FSEntry x, FSEntry y) => string.Compare(x.Name, y.Name, true);
        }


        public static string ParentDir(string path)
        {
            switch (path)
            {
                case "/--":
                    return "/\\";
                case "/\\":
                    return "/";
                case "/":
                    return "/--";
                case "/--\\":
                    return "/--";
            }

            try
            {
                return Directory.GetParent(path).FullName;
            }
            catch (System.Exception)
            {
                return "/--";
            }
        }

        public static string NavigateText(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return path;
                }
                else
                {
                    while (!Directory.Exists(path))
                    {
                        path = Directory.GetParent(path).FullName;
                    }

                    return path;
                }
            }
            catch (System.Exception)
            {
                return "/--";
            }

        }


        public static FSFileAttrib FileType(string e)
        {
            if (e.Length == 0) return FSFileAttrib.Other;
            if (Ext2Type.TryGetValue(e.ToLower(), out FSFileAttrib attr))
            {
                return attr;
            }
            else
            {
                return FSFileAttrib.Other;
            }
        }

        public static FileInfo GetFileInfo(string path)
        {
            try
            {
                return new FileInfo(path);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static DirectoryInfo GetDirectoryInfo(string path)
        {
            try
            {
                return new DirectoryInfo(path);
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static FileOperationResult NewFolder(string path)
        {
            var res = new FileOperationResult();
            try
            {
                if (Directory.Exists(path)) res = new FileOperationResult() { Success = false };
                else
                {
                    Directory.CreateDirectory(path);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            return res;
        }
        public static FileOperationResult DeleteFolder(string path)
        {
            var res = new FileOperationResult();
            try
            {
                if (Directory.Exists(path)) res = new FileOperationResult() { Success = false };
                else
                {
                    Directory.Delete(path);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            return res;
        }
        public static FileOperationResult CopyPasteFolder(string Source, string Dest, bool ReplaceIfExists)
        {
            var res = new FileOperationResult();
            try
            {
                if (Directory.Exists(Source)) res = new FileOperationResult() { Success = false };
                else if (!ReplaceIfExists && Directory.Exists(Dest)) res = new FileOperationResult() { Success = false };
                else
                {
                    // Directory.Copy(Source, Dest, ReplaceIfExists);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            return res;
        }
        public static FileOperationResult CutPasteFolder(string Source, string Dest, bool ReplaceIfExists)
        {
            var res = new FileOperationResult();
            try
            {
                if (Directory.Exists(Source)) res = new FileOperationResult() { Success = false };
                else if (!ReplaceIfExists && Directory.Exists(Dest)) res = new FileOperationResult() { Success = false };
                else
                {
                    Directory.Move(Source, Dest);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            return res;
        }


        public static FileOperationResult NewFile(string Name)
        {
            var res = new FileOperationResult();
            try
            {
                if (File.Exists(Name)) res = new FileOperationResult() { Success = false };
                else
                {
                    File.Create(Name).Dispose();

                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            // catch (UnauthorizedAccessException e)
            // {
            //     res = new FileOperationResult() { Success = false, };
            // }
            // catch (ArgumentException e)
            // {
            //     res = new FileOperationResult() { Success = false, };
            // }
            // catch (PathTooLongException e)
            // {
            //     res = new FileOperationResult() { Success = false, };
            // }
            // catch (DirectoryNotFoundException e)
            // {
            //     res = new FileOperationResult() { Success = false, };
            // }
            // catch (NotSupportedException e)
            // {
            //     res = new FileOperationResult() { Success = false, };
            // }
            return res;
        }
        public static FileOperationResult DeleteFile(string Name)
        {
            var res = new FileOperationResult();
            try
            {
                if (File.Exists(Name)) res = new FileOperationResult() { Success = false };
                else
                {
                    File.Delete(Name);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            return res;
        }
        public static FileOperationResult CopyPasteFile(string Source, string Dest, bool ReplaceIfExists)
        {
            var res = new FileOperationResult();
            try
            {
                if (File.Exists(Source)) res = new FileOperationResult() { Success = false };
                else if (!ReplaceIfExists && File.Exists(Dest)) res = new FileOperationResult() { Success = false };
                else
                {
                    File.Copy(Source, Dest, ReplaceIfExists);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            return res;
        }
        public static FileOperationResult CutPasteFile(string Source, string Dest, bool ReplaceIfExists)
        {
            var res = new FileOperationResult();
            try
            {
                if (File.Exists(Source)) res = new FileOperationResult() { Success = false };
                else if (!ReplaceIfExists && File.Exists(Dest)) res = new FileOperationResult() { Success = false };
                else
                {
                    File.Move(Source, Dest, ReplaceIfExists);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (Exception e)
            {
                Core.LogErr(e);
                res = new FileOperationResult() { Success = false, };
            }
            return res;
        }

        private static readonly Dictionary<string, FSFileAttrib> Ext2Type = new Dictionary<string, FSFileAttrib>()
        {
            {".exe", FSFileAttrib.Excecutable},
            {".msi", FSFileAttrib.Excecutable},
            {".rpm", FSFileAttrib.Excecutable},
            {".dmg", FSFileAttrib.Excecutable},
            {".deb", FSFileAttrib.Excecutable},
            {".appimage", FSFileAttrib.Excecutable},
            {".bat", FSFileAttrib.Excecutable | FSFileAttrib.Text | FSFileAttrib.SourceCode},
            {".sh", FSFileAttrib.Excecutable | FSFileAttrib.Text | FSFileAttrib.SourceCode},
            {".py", FSFileAttrib.Excecutable | FSFileAttrib.Text | FSFileAttrib.SourceCode},
            {".jar", FSFileAttrib.Excecutable},
            {".dll", FSFileAttrib.Excecutable},
            {".mp4", FSFileAttrib.Video},
            {".mpeg", FSFileAttrib.Video},
            {".mov", FSFileAttrib.Video},
            {".mpg", FSFileAttrib.Video},
            {".mkv", FSFileAttrib.Video},
            {".webm", FSFileAttrib.Video},
            {".avi", FSFileAttrib.Video},
            {".mp3", FSFileAttrib.Audio},
            {".wav", FSFileAttrib.Audio},
            {".ogg", FSFileAttrib.Audio},
            {".jpg", FSFileAttrib.Image},
            {".jpeg", FSFileAttrib.Image},
            {".png", FSFileAttrib.Image},
            {".gif", FSFileAttrib.Image},
            {".svg", FSFileAttrib.Image},
            {".webp", FSFileAttrib.Image},
            {".bmp", FSFileAttrib.Image},
            {".tiff", FSFileAttrib.Image},
            {".pdf", FSFileAttrib.Document},
            {".xml", FSFileAttrib.Document | FSFileAttrib.Text | FSFileAttrib.SourceCode},
            {".html", FSFileAttrib.Document | FSFileAttrib.Text | FSFileAttrib.SourceCode},
            {".mhtml", FSFileAttrib.Document | FSFileAttrib.Text},
            {".md", FSFileAttrib.Document | FSFileAttrib.Text | FSFileAttrib.SourceCode},
            {".txt", FSFileAttrib.Text},
            {".json", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".log", FSFileAttrib.Text},
            {".zip", FSFileAttrib.Archive},
            {".rar", FSFileAttrib.Archive},
            {".gzip", FSFileAttrib.Archive},
            {".7z", FSFileAttrib.Archive},
            {".tar", FSFileAttrib.Archive},
            {".iso", FSFileAttrib.Archive},
            {".ppt", FSFileAttrib.Presentation},
            {".pptx", FSFileAttrib.Presentation},
            {".odp", FSFileAttrib.Presentation},
            {".doc", FSFileAttrib.OfficeDocument},
            {".docx", FSFileAttrib.OfficeDocument},
            {".odf", FSFileAttrib.OfficeDocument},
            {".rtf", FSFileAttrib.OfficeDocument},
            {".xls", FSFileAttrib.SpreadSheet},
            {".xlsx", FSFileAttrib.SpreadSheet},
            {".ods", FSFileAttrib.SpreadSheet},
            {".php", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".css", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".js", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".c", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".h", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".cpp", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".cs", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".vb", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".vbs", FSFileAttrib.SourceCode | FSFileAttrib.Text},
            {".ino", FSFileAttrib.SourceCode | FSFileAttrib.Text},
        };

        public static readonly Dictionary<string, string> Ext2Mime = new Dictionary<string, string>()
         {
             {".flv", "video/x-flv"},
             {".mp4", "video/mp4"},
             {".m3u8", "video/x-mpegURL"},
             {".ts", "video/mp2t"},
             {".3gp", "video/3gpp"},
             {".mov", "video/quicktime"},
             {".avi", "video/x-msvideo"},
             {".wmv", "video/x-ms-wmv"},
             {".mpeg", "video/mpeg"},
             {".webm", "video/webm"},
             {".mkv", "video/webm"}
         };
    }




}
