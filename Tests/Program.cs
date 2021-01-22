using System;
using System.IO;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ex();
            Console.ReadLine();
        }
        static async void ex()
        {
            Directory.CreateDirectory("workspace/");
            Environment.CurrentDirectory = "workspace/";

            System.Console.WriteLine("Checking FFmpeg");

            await AirFFmpeg.Init();

            System.Console.WriteLine("FFmpeg OK");
            System.Console.WriteLine(Path.GetFullPath(Environment.CurrentDirectory));



            string file = @"vidusaviya.mp4";
            if (File.Exists(file))
            {
                System.Console.WriteLine("File Exists OK");
            }

            await (await FFmpeg.Conversions.FromSnippet.Convert(file, "vidusaviya.webm")).Start();

            System.Console.WriteLine("File Converted");

            Console.ReadLine();
        }



    }

    public static class AirFFmpeg
    {
        public static bool IsLoaded = false;

        public static async Task Init()
        {
            IsLoaded = false;

            string directoryWithFFmpegAndFFprobe = Path.Combine(Environment.CurrentDirectory, "AirFFmpeg");
            FFmpeg.SetExecutablesPath(directoryWithFFmpegAndFFprobe, "ffmpeg", "ffprobe");

            if (File.Exists(Path.Combine(directoryWithFFmpegAndFFprobe, "ffmpeg")))
            {
                IsLoaded = true;

                return;
            }

            await InstallFF();


        }

        public static async Task InstallFF()
        {
            IsLoaded = false;
            try
            {
                System.Console.WriteLine("Downloading FFmpeg...");
                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
                IsLoaded = true;
            }
            catch (System.Exception)
            {

                throw;
            }


        }

    }

}


