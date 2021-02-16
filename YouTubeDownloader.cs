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
                // OnDownloading?.Invoke((DownloadInfo)sender);
            };
            client.StandardErrorEvent += (sender, error) => OnError?.Invoke(error);
            client.StandardOutputEvent += (sender, output) =>
            {
                var res = output.Split(" ").ToList();
                res.RemoveAll(s => string.IsNullOrWhiteSpace(s));
                if (res.Count == 8)
                {
                    OnDownloading?.Invoke(new DownloadInfo { DownloadRate = res[5], Eta = res[7], VideoSize = res[3], Status = res[1] });
                }
                OnLog?.Invoke(output);
            };
        }
        public async Task<DownloadInfo> GetVideoInfo(string url)
        {
            if (!IsVaildYoutubeUrl(url)) return null;
            return await client.GetDownloadInfoAsync(url);
        }
        public async void DownloadVideo(string url, string outputpath, Enums.VideoFormat videoFormat, Enums.AudioFormat audioFormat = Enums.AudioFormat.best)
        {
            if (!IsVaildYoutubeUrl(url)) return;
            client.Options.VideoFormatOptions.Format = videoFormat;
            client.Options.PostProcessingOptions.AudioFormat = audioFormat;
            var info = await GetVideoInfo(url);
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
            await client.DownloadAsync(url);
        }
        public static bool IsVaildYoutubeUrl(string youTubeURl)
        {
            string pattern = "^(http(s)?:\\/\\/)?((w){3}.)?youtu(be|.be)?(\\.com)?\\/.+";
            return (!string.IsNullOrWhiteSpace(youTubeURl) && Regex.IsMatch(youTubeURl, pattern));
        }
        public static string GetEmbedURL(string youTubeURl)
        {
            return youTubeURl.Replace("watch?v=", @"embed/");
        }
        public static string GetThumbnail(string YoutubeUrl)
        {
            string youTubeThumb = string.Empty;
            if (YoutubeUrl == "")
                return "";

            if (YoutubeUrl.IndexOf("=") > 0)
            {
                youTubeThumb = YoutubeUrl.Split('=')[1];
            }
            else if (YoutubeUrl.IndexOf("/v/") > 0)
            {
                string strVideoCode = YoutubeUrl.Substring(YoutubeUrl.IndexOf("/v/") + 3);
                int ind = strVideoCode.IndexOf("?");
                youTubeThumb = strVideoCode.Substring(0, ind == -1 ? strVideoCode.Length : ind);
            }
            else if (YoutubeUrl.IndexOf('/') < 6)
            {
                youTubeThumb = YoutubeUrl.Split('/')[3];
            }
            else if (YoutubeUrl.IndexOf('/') > 6)
            {
                youTubeThumb = YoutubeUrl.Split('/')[1];
            }

            return "http://img.youtube.com/vi/" + youTubeThumb + "/mqdefault.jpg";
        }
    }
}
