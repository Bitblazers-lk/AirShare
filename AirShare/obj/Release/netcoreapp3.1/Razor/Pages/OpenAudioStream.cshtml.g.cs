#pragma checksum "D:\My Projects\C# Git\AirShare\Pages\OpenAudioStream.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2238f4366f556542134824cf6d6721e1eb7e26c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AirShare.Pages.Pages_OpenAudioStream), @"mvc.1.0.razor-page", @"/Pages/OpenAudioStream.cshtml")]
namespace AirShare.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 3 "D:\My Projects\C# Git\AirShare\Pages\OpenAudioStream.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\My Projects\C# Git\AirShare\Pages\OpenAudioStream.cshtml"
using System.IO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "/OpenAudioStream/{PFileName}")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2238f4366f556542134824cf6d6721e1eb7e26c8", @"/Pages/OpenAudioStream.cshtml")]
    public class Pages_OpenAudioStream : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 9 "D:\My Projects\C# Git\AirShare\Pages\OpenAudioStream.cshtml"
  

    //  [Parameter] string PFileName ;

    Response.Headers.Clear();
    // HttpContext.Response.Clear();
    string url;
    long time = 0;
    long Pos = -1;
    try
    {

        //Console.WriteLine($"Partial Request {Request.Headers["Range"]}");

        string q = HttpContext.Request.QueryString.Value.TrimStart('?');
        int splitAt = q.IndexOf(':');
        if (splitAt != -1)
        {
            if (long.TryParse(q.Substring(0, splitAt), out time))
            {
                url = Uri.UnescapeDataString(q.Substring(splitAt + 1)).Trim('"');
            }
            else
            {
                time = 0;
                url = Uri.UnescapeDataString(q.TrimStart('?')).Trim('"');
            }
        }
        else
        {
            time = 0;
            url = Uri.UnescapeDataString(q.TrimStart('?')).Trim('"');
        }



    }
    catch (Exception ex)
    {
        //TODO: Use more understandable err msgs
        Console.WriteLine("Path error " + ex.Message);
        await Response.WriteAsync("Path error " + ex.Message);
        return;
    }


    
    try
    {
        User usr = Core.AuthReq(Request , Response);
        if(usr == null)
        {
            Console.WriteLine($"OpenAudioStream not logged in ");
            await Response.WriteAsync("OpenAudioStream not logged in" );
            return;
        }

        if(!usr.Validate(url))
        {
            Console.WriteLine($"OpenAudioStream {usr?.Name} ({usr?.Lvl}) cannot access {url}");
            await Response.WriteAsync("You cannot access this " );
            return;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("OpenAudioStream Cookie Auth error " + ex.Message);
        await Response.WriteAsync("Auth error " + ex.Message);
        return;
    }


    if (Request.Headers.TryGetValue("Range", out Microsoft.Extensions.Primitives.StringValues sval))
    {
        string rs = sval.ToString();
        if (rs.Length > 0)
        {
            string[] parts = rs.Split('=', '-');
            if (parts.Length > 2)
            {
                if (long.TryParse(parts[1], out long newPos))
                {
                    Pos = newPos;
                }
            }
        }
    }




    FileStream FS;
    try
    {
        FS = System.IO.File.Open(url, FileMode.Open, FileAccess.Read, FileShare.Read);
    }
    catch (Exception ex)
    {
        //TODO: Use more understandable err msgs
        Console.WriteLine("Opening error " + url + " >> " + ex.Message);
        await Response.WriteAsync("Opening error " + url + " >> " + ex.Message);
        return;
    }
    long L = FS.Length;

    if (Pos > 0 && FS.CanSeek)
    {
        FS.Seek(Pos, SeekOrigin.Begin);
        Response.StatusCode = 206;
        Response.Headers.Add("Content-Range", $" bytes {Pos}-{L - 1}/{L}");
        time = Pos * 100L / L;
    }
    else if (time > 0 && FS.CanSeek)
    {
        Pos = (long)(L * (time / 100f));
        FS.Seek(Pos, SeekOrigin.Begin);
        Response.StatusCode = 206;
    }
    else
    {
        Pos = 0;
    }

    Console.WriteLine($"Streaming video {url} \t at {time}% \t ({Pos} of {L})");

    Response.Headers.Add("Accept-Ranges", "bytes");
    Response.ContentType = "audio/mp3";
    Response.Headers.Add("Content-Length", (L - Pos + 4).ToString());
    Response.Headers.Add("Access-Control-Allow-Origin", "*");

    // HttpContext.Response.Headers.Clear();
    // HttpContext.Response.Body = new MemoryStream();
    await FS.CopyToAsync(HttpContext.Response.BodyWriter.AsStream(true));
    Console.WriteLine($"Done Streaming {url} \t at {time}% \t ({Pos} of {L})");
    FS.Dispose();

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Components.NavigationManager NavMan { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pages_OpenAudioStream> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_OpenAudioStream> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_OpenAudioStream>)PageContext?.ViewData;
        public Pages_OpenAudioStream Model => ViewData.Model;
    }
}
#pragma warning restore 1591
