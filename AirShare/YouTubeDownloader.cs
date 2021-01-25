using NYoutubeDL;
using NYoutubeDL.Helpers;
using NYoutubeDL.Models;
using NYoutubeDL.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirShare
{
    public class YoutubeDownloader
    {
        YoutubeDL client;
        public Action<DownloadInfo> OnDownloading { get; set; }
        public Action<string> OnError { get; set; }
        public Action<string> OnLog { get; set; }
        public YoutubeDownloader()
        {
            client = new YoutubeDL();
            client.YoutubeDlPath = @"youtube-dl.exe";
            client.Options.DownloadOptions.FragmentRetries = -1;
            client.Options.DownloadOptions.Retries = -1;
            client.Options.PostProcessingOptions.AudioQuality = "0";
            client.Info.PropertyChanged += (sender, e) =>
            {
                OnDownloading?.Invoke((DownloadInfo)sender);
            };
            client.StandardErrorEvent += (sender, error) => OnError?.Invoke(error);
            client.StandardOutputEvent += (sender, output) => OnLog?.Invoke(output);
        }
        public DownloadInfo GetVideoInfo(string url)
        {
            if (!IsVaildYoutubeUrl(url)) return null;
            return client.GetDownloadInfo(url);
        }
        public void DownloadVideo(string url, string outputpath, Enums.VideoFormat videoFormat, Enums.AudioFormat audioFormat = Enums.AudioFormat.best)
        {
            if (!IsVaildYoutubeUrl(url)) return;
            client.Options.VideoFormatOptions.Format = videoFormat;
            client.Options.PostProcessingOptions.AudioFormat = audioFormat;
            var info = GetVideoInfo(url);
            if (info == null) return;
            string tmpp = Path.Combine(outputpath, $"{info.Title}.mp4");
            int i = 0;
            while (File.Exists(tmpp))
            {
                i++;
                tmpp = Path.Combine(outputpath, $"{info.Title}({i}).mp4");
            }
            client.Options.FilesystemOptions.Output = tmpp;
            client.Options = Options.Deserialize(client.Options.Serialize());
            client.Download(url);
        }
        public static bool IsVaildYoutubeUrl(string youTubeURl)
        {
            string pattern = "^(http(s)?:\\/\\/)?((w){3}.)?youtu(be|.be)?(\\.com)?\\/.+";
            return (!string.IsNullOrWhiteSpace(youTubeURl) && Regex.IsMatch(youTubeURl, pattern));
        }
        public static string GetEmbedURL(string youTubeURl)
        {
           return youTubeURl.Replace("watch?v=",@"embed/");
        }
    }
}
