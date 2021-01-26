using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string s= "[youtube] IXvjOjVZjTM: Downloading webpage\n"
                    + "[download] Destination: F:text.mp3\n"
                    + "[download]   0.0% of 10.99MiB at  2.30KiB/s ETA 01:21:39\n"
                    + "[download]   0.0% of 10.99MiB at  6.89KiB/s ETA 27:12\n"
                    + "[download]   0.1% of 10.99MiB at 16.07KiB/s ETA 11:39\n"
                    + "[download]   0.1% of 10.99MiB at 33.29KiB/s ETA 05:37\n"
                    + "[download]   0.3% of 10.99MiB at 66.20KiB/s ETA 02:49\n"
                    + "[download]   0.6% of 10.99MiB at 134.32KiB/s ETA 01:23\n"
                    + "[download]   1.1% of 10.99MiB at 261.67KiB/s ETA 00:42\n"
                    + "[download]   2.3% of 10.99MiB at 505.42KiB/s ETA 00:21\n"
                    + "[download]   4.5% of 10.99MiB at 626.07KiB/s ETA 00:17\n"
                    + "[download]   9.1% of 10.99MiB at 723.16KiB/s ETA 00:14\n"
                    + "[download]  16.7% of 10.99MiB at 808.82KiB/s ETA 00:11\n"
                    + "[download]  25.1% of 10.99MiB at 827.22KiB/s ETA 00:10\n"
                    + "[download]  32.8% of 10.99MiB at 849.89KiB/s ETA 00:08\n"
                    + "[download]  41.1% of 10.99MiB at 909.64KiB/s ETA 00:07\n"
                    + "[download]  52.3% of 10.99MiB at 900.87KiB/s ETA 00:05\n"
                    + "[download]  60.0% of 10.99MiB at 886.97KiB/s ETA 00:05\n"
                    + "[download]  67.1% of 10.99MiB at 901.02KiB/s ETA 00:04\n"
                    + "[download]  76.4% of 10.99MiB at 888.03KiB/s ETA 00:02\n"
                    + "[download]  83.5% of 10.99MiB at 871.25KiB/s ETA 00:02\n"
                    + "[download]  90.0% of 10.99MiB at 868.62KiB/s ETA 00:01\n"
                    + "[download]  97.4% of 10.99MiB at 866.12KiB/s ETA 00:00\n"
                    + "[download] 100.0% of 10.99MiB at 872.51KiB/s ETA 00:00\n"
                    + "[download] 100% of 10.99MiB in 00:13";

            foreach (var item in s.Split('\n'))
            {
                var res = item.Split(" ").ToList();
                res.RemoveAll(s => string.IsNullOrWhiteSpace(s));
                Console.WriteLine(res.Count);
            }
            // ex();
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


