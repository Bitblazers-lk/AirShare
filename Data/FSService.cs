using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AirShare
{
    public static class FSService
    {


        public static DirectoryEntries ListFiles(string path)
        {
            if (path == "\\")
            {
                DirectoryEntries D = new DirectoryEntries() { Path = "", LA = DateTime.Now };

                D.SubDirs.Add(new FSEntry() { Name = Core.CreateAirSharedDir(), Atrb = FSFileAttrib.Directory });

                foreach (string item in Directory.GetLogicalDrives())
                {
                    D.SubDirs.Add(new FSEntry() { Name = item, Atrb = FSFileAttrib.Directory });
                }
                return D;

            }
            // else if (path.StartsWith("-/"))
            // {
            //     ResolveVPath(path);
            // }
            else
            {
                return ListFilesAbs(path);
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

            D.LA = DI.LastAccessTime;
            D.Path = DI.FullName;

            Core.Log($"cd {path}");

            return D;
        }


        public static string ParentDir(string path)
        {
            try
            {
                return Directory.GetParent(path).FullName;
            }
            catch (System.Exception)
            {
                return "\\";
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
                return "\\";
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
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
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
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
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
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
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
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
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
                    File.Create(Name);
                    res = new FileOperationResult() { Code = FileOperationCode.Success, Success = true };
                }
            }
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
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
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
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
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
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
            catch (UnauthorizedAccessException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (ArgumentException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (PathTooLongException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (DirectoryNotFoundException e)
            {
                res = new FileOperationResult() { Success = false, };
            }
            catch (NotSupportedException e)
            {
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
    }


}
