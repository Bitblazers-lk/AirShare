#pragma checksum "D:\My Projects\C# Git\AirShare\Pages\ListFiles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12cf3c54f7a13509e395e8c1fcd69f22384bf9a9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AirShare.Pages.Pages_ListFiles), @"mvc.1.0.razor-page", @"/Pages/ListFiles.cshtml")]
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
#line 3 "D:\My Projects\C# Git\AirShare\Pages\ListFiles.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\My Projects\C# Git\AirShare\Pages\ListFiles.cshtml"
using System.IO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "/ListFiles")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12cf3c54f7a13509e395e8c1fcd69f22384bf9a9", @"/Pages/ListFiles.cshtml")]
    public class Pages_ListFiles : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 9 "D:\My Projects\C# Git\AirShare\Pages\ListFiles.cshtml"
  
    Response.Headers.Clear();
    // HttpContext.Response.Clear();
    string url;

    try
    {

        //Console.WriteLine($"Partial Request {Request.Headers["Range"]}");

        string q = HttpContext.Request.QueryString.Value.TrimStart('?');

        url = Uri.UnescapeDataString(q.TrimStart('?')).Trim('"');


    }
    catch (Exception ex)
    {
        Console.WriteLine("ListFiles Path error " + ex.Message);
        await Response.WriteAsync("Path error " + ex.Message);
        return;
    }

    User usr;
    try
    {
        usr = Core.AuthReq(Request, Response);
        if (usr == null)
        {
            Console.WriteLine($"ListFiles not logged in ");
            await Response.WriteAsync("ListFiles not logged in");
            return;
        }

        if (!usr.Validate(url, FSPermission.Read))
        {
            Console.WriteLine($"OpenAudioStream {usr?.Name} ({usr?.Lvl}) cannot access {url}");
            await Response.WriteAsync("You cannot access this ");
            return;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("ListFiles Cookie Auth error " + ex.Message);
        await Response.WriteAsync("Auth error " + ex.Message);
        return;
    }


    try
    {
        await Response.WriteAsync(Core.ToJSON(FSService.ListFiles(url, usr)));
    }
    catch (Exception ex)
    {
        //TODO: Use more understandable err msgs
        Console.WriteLine("ListFiles Opening error " + url + " >> " + ex.Message);
        await Response.WriteAsync("Opening error " + url + " >> " + ex.Message);
        return;
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pages_ListFiles> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_ListFiles> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_ListFiles>)PageContext?.ViewData;
        public Pages_ListFiles Model => ViewData.Model;
    }
}
#pragma warning restore 1591