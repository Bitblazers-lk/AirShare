using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirShare.Data
{
    public static class FileTypeIconConverter
    {
        public static FileTypeIcon GetFileTypeIcon(string ext)
        {
            if (Enum.TryParse(ext, true, out FileTypeIcon res)) return res;
            else if (
                 ext.Equals("mp3", StringComparison.CurrentCultureIgnoreCase) ||
                 ext.Equals("WAV", StringComparison.CurrentCultureIgnoreCase) ||
                 ext.Equals("weba", StringComparison.CurrentCultureIgnoreCase) ||
                 ext.Equals("mp3", StringComparison.CurrentCultureIgnoreCase) ||
                 ext.Equals("mp3", StringComparison.CurrentCultureIgnoreCase)
                 ) return FileTypeIcon.music;
            else if (
                ext.Equals("mp4", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("m4p", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("webm", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("ogg", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("MPG", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("MP2", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("MPEG", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("MPE", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("MPV", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("MKV", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("M4V", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("AVI", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("WMV", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("MOV", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("FLV", StringComparison.CurrentCultureIgnoreCase)
                ) return FileTypeIcon.video;
            else if (
                ext.Equals("JPG", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("PNG", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("GIF", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("WEBP", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("TIFF", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("PSD", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("RAW", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("BMP", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals("SVG", StringComparison.CurrentCultureIgnoreCase)
     ) return FileTypeIcon.image;
            else return FileTypeIcon.unknown;
        }
    }

    public enum FileTypeIcon
    {
        docx,
        doc,
        pptx,
        ppt,
        xlsx,
        xls,
        pdf,
        image,
        rar,
        zip,
        txt,
        music,
        video,
        js,
        css,
        html,
        php,
        exe,
        msi,
        unknown,
        rtf,
        xml,
        folder
    }
}
