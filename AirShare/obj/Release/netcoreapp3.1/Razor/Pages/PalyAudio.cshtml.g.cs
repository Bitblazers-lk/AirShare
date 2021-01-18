#pragma checksum "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "abeb43c9d4f6891b042e8cf55eccbe357b195fee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AirShare.Pages.Pages_PalyAudio), @"mvc.1.0.razor-page", @"/Pages/PalyAudio.cshtml")]
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
#line 3 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
using System.IO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "/Playaudio/{PFileName}")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"abeb43c9d4f6891b042e8cf55eccbe357b195fee", @"/Pages/PalyAudio.cshtml")]
    public class Pages_PalyAudio : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n");
#nullable restore
#line 12 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
  

    string RedirectPath = "/?Playaudio/" + RouteData.Values["PFileName"] + HttpContext.Request.QueryString.Value;
    bool Failed = false;

    try
    {
        User usr = Core.AuthReq(Request , Response);
        if(usr == null)
        {
            Console.WriteLine($"Playaudio not logged in ");
            Failed = true;
        }

        string url;
    
        try
        {
            
            //Console.WriteLine($"Partial Request {Request.Headers["Range"]}");

            string q = HttpContext.Request.QueryString.Value.TrimStart('?');
        
            url = Uri.UnescapeDataString(q.TrimStart('?')).Trim('"');
            
            if(!usr.Validate(url))
            {
                Console.WriteLine($"Playaudio {usr?.Name} ({usr?.Lvl}) cannot access {url}");
                Failed = true;
                
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Playaudio Path error " + ex.Message);           
            Failed = true;
        }

      
    }
    catch (Exception ex)
    {
        Console.WriteLine("Playaudio Cookie Auth error " + ex.Message);
        Failed = true;
    }

    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 64 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
 if(Failed)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <a");
            BeginWriteAttribute("href", " href=\"", 1498, "\"", 1518, 1);
#nullable restore
#line 66 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
WriteAttributeValue("", 1505, RedirectPath, 1505, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Click here to login</a>\r\n");
#nullable restore
#line 67 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<audio controls autoplay style=\"width:100%;height:100%;\">\r\n    <source");
            BeginWriteAttribute("src", " src=\"", 1627, "\"", 1729, 1);
#nullable restore
#line 71 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
WriteAttributeValue("", 1633, "/OpenAudioStream/" +  RouteData.Values["PFileName"] + HttpContext.Request.QueryString.Value , 1633, 96, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"audio/mp3\">\r\n    Your browser does not support the audio tag.\r\n</audio>\r\n");
#nullable restore
#line 74 "D:\My Projects\C# Git\AirShare\Pages\PalyAudio.cshtml"
}

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pages_PalyAudio> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_PalyAudio> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_PalyAudio>)PageContext?.ViewData;
        public Pages_PalyAudio Model => ViewData.Model;
    }
}
#pragma warning restore 1591
