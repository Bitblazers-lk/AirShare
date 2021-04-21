using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirShare.Pages.Controls
{
    [ApiController]
    [Route("")]
    public class FileController : ControllerBase
    {
        public readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }


        [HttpGet("hi")]
        public string Hi()
        {
            return "Hi there";
        }




        [HttpGet("OpenFile/{QFileName}")]
        public FileResult Download()
        {

            string url = Uri.UnescapeDataString(HttpContext.Request.QueryString.Value.TrimStart('?')).Trim('"');


            try
            {
                User usr = Core.AuthReq(Request, Response);
                if (usr == null)
                {
                    Console.WriteLine($"OpenFile not logged in ");
                    return default(FileResult);
                }

                if (!usr.Validate(url, FSPermission.Read))
                {
                    Console.WriteLine($"OpenFile {usr?.Name} ({usr?.Lvl}) cannot access {url}");
                    return default(FileResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OpenFile Cookie Auth error " + ex.Message);
                return default(FileResult);
            }

            Console.WriteLine("Downloading " + url);

            FileInfo file = new FileInfo(url);
            return PhysicalFile(file.FullName, System.Net.Mime.MediaTypeNames.Application.Octet, file.Name, true);
        }


        [HttpGet("/OpenVideoStream/{QFileName}")]
        [HttpGet("/OpenAudioStream/{QFileName}")]
        public FileResult OpenVideoStream()
        {

            string url = Uri.UnescapeDataString(HttpContext.Request.QueryString.Value.TrimStart('?')).Trim('"');


            try
            {
                User usr = Core.AuthReq(Request, Response);
                if (usr == null)
                {
                    Console.WriteLine($"OpenVideoStream not logged in ");
                    return default(FileResult);
                }

                if (!usr.Validate(url, FSPermission.Read))
                {
                    Console.WriteLine($"OpenVideoStream {usr?.Name} ({usr?.Lvl}) cannot access {url}");
                    return default(FileResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OpenVideoStream Cookie Auth error " + ex.Message);
                return default(FileResult);
            }

            Console.WriteLine("OpenVideoStream " + url);

            FileInfo file = new FileInfo(url);
            if (!FSService.Ext2Mime.TryGetValue(file.Extension.ToLower(), out string mime))
            {
                mime = "video/webm";
            }

            return PhysicalFile(file.FullName, mime, file.Name, true);
        }




        [HttpGet("/OpenStream/{QMime}/{QFileName}")]
        [HttpGet("/OpenStream/{QFileName}")]
        public IActionResult OpenStream(string QMime)
        {
            string url = Uri.UnescapeDataString(HttpContext.Request.QueryString.Value.TrimStart('?')).Trim('"');


            try
            {
                User usr = Core.AuthReq(Request, Response);
                if (usr == null)
                {
                    Console.WriteLine($"OpenStream not logged in ");
                    return default(FileResult);
                }

                if (!usr.Validate(url, FSPermission.Read))
                {
                    Console.WriteLine($"OpenStream {usr?.Name} ({usr?.Lvl}) cannot access {url}");
                    return default(FileResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("OpenStream Cookie Auth error " + ex.Message);
                return default(FileResult);
            }

            Console.WriteLine("OpenStream " + url);

            FileInfo file = new FileInfo(url);

            string mime;


            switch (QMime)
            {
                case "image":
                    if (!FSService.Ext2Mime.TryGetValue(file.Extension.ToLower(), out mime))
                    {
                        mime = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                    }
                    break;

                case "video":
                    if (!FSService.Ext2Mime.TryGetValue(file.Extension.ToLower(), out mime))
                    {
                        mime = "video/webm";
                    }
                    break;

                case "audio":
                    if (!FSService.Ext2Mime.TryGetValue(file.Extension.ToLower(), out mime))
                    {
                        mime = "audio/webm";
                    }
                    break;

                case "document":
                case "text":
                    mime = System.Net.Mime.MediaTypeNames.Text.Plain;
                    break;

                case "auto":
                default:
                    if (!FSService.Ext2Mime.TryGetValue(file.Extension.ToLower(), out mime))
                    {
                        mime = System.Net.Mime.MediaTypeNames.Text.Plain;
                    }
                    break;

            }


            return PhysicalFile(file.FullName, mime, true);
        }

    }
}
