using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace AirShare
{
    public static class AVC
    {
        public static async Task<IConversion> ExtractAudio(string In,string Out)
        {
           return await FFmpeg.Conversions.FromSnippet.ExtractAudio(In, Out);
        }
        public static async Task<IMediaInfo> GetMediaInfo(string Path)
        {
            return await FFmpeg.GetMediaInfo(Path);
        }
    }
}
