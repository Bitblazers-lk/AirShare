using System;
using System.IO;
using Xabe.FFmpeg;

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
            FFmpeg.SetExecutablesPath(Environment.CurrentDirectory, ffmpegExeutableName: "FFmpeg");
            string output = @"E:\Artists\2XO\2XO - Blame It On You (Official Music Video).mp3";
            IConversionResult result = await Conversion.ExtractAudio(@"E:\Artists\2XO\2XO - Blame It On You (Official Music Video).mp4", output).Start();
          var res= await  FFmpeg.GetMediaInfo("");
            
            Console.ReadLine();
        }
    }
}
