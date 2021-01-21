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
            IConversionResult result = await Conversion.ExtractAudio(@"E:\Artists\2XO\2XO - Blame It On You (Official Music Video).mp4", @"D:\My Projects\C# Git\AirShare\AirShare\Tmp\2XO - Summer Love ft. Ricky Garcia.mp3").Start();
            
            
            Console.ReadLine();
        }
    }
}
