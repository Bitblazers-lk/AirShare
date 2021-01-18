#pragma checksum "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea7550ea9a324c20f0bbdae56754ba05d4e26c1c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AirShare.Pages.Pages_PlayVideo), @"mvc.1.0.razor-page", @"/Pages/PlayVideo.cshtml")]
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
#line 3 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
using System.IO;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "/PlayVideo/{PFileName}")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea7550ea9a324c20f0bbdae56754ba05d4e26c1c", @"/Pages/PlayVideo.cshtml")]
    public class Pages_PlayVideo : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("margin:0px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n\r\n\r\n");
#nullable restore
#line 11 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
  
    string RedirectPath = "/?PlayVideo/" + RouteData.Values["PFileName"] + HttpContext.Request.QueryString.Value;
    bool Failed = false;
    Dictionary<string, string> Subs = new Dictionary<string, string>();

    try
    {
        User usr = Core.AuthReq(Request, Response);
        if (usr == null)
        {
            Console.WriteLine($"PlayVideo not logged in ");
            Failed = true;
        }

        string url;

        try
        {

            //Console.WriteLine($"Partial Request {Request.Headers["Range"]}");

            string q = HttpContext.Request.QueryString.Value.TrimStart('?');

            url = Uri.UnescapeDataString(q.TrimStart('?')).Trim('"');

            if (!usr.Validate(url, FSPermission.Read))
            {
                Console.WriteLine($"PlayVideo {usr?.Name} ({usr?.Lvl}) cannot access {url}");
                Failed = true;

            }
            else
            {
                DirectoryInfo PDInfo = System.IO.Directory.GetParent(url);
                foreach (FileInfo FI in PDInfo.EnumerateFiles())
                {
                    string ext = FI.Extension.ToLower();
                    if (ext == ".srt" || ext == ".vtt")
                    {
                        Subs.Add(System.IO.Path.GetFileName(FI.Name), FI.FullName);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("PlayVideo Path error " + ex.Message);
            Failed = true;
        }


    }
    catch (Exception ex)
    {
        Console.WriteLine("PlayVideo Cookie Auth error " + ex.Message);
        Failed = true;
    }



#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<html>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea7550ea9a324c20f0bbdae56754ba05d4e26c1c5372", async() => {
                WriteLiteral(@"
    <script lang=""text/javascript"">
        document.addEventListener(""DOMContentLoaded"", function () { var videoElements = document.getElementsByTagName('video'); function convertSrtToVtt() { this.id = '_' + Math.random().toString(36).substr(2, 9); var self = document.getElementById(this.id); var tracks = document.querySelectorAll(""#"" + this.id + "" track""); var subtitle = { data: { track: {} }, load: function (track) { subtitle.track = track; if (subtitle.isSrt(subtitle.track.src)) { var client = new XMLHttpRequest(); client.open('GET', subtitle.track.src); client.onreadystatechange = function () { subtitle.convert(client.responseText).then(function (file) { subtitle.track.src = file }) }; client.send() } }, convert: function (content) { var promise = new Promise(function (resolve, reject) { content = content.replace(/(\d+:\d+:\d+)+,(\d+)/g, '$1.$2'); content = ""WEBVTT - Generated using SRT2VTT\r\n\r\n"" + content; var blob = new Blob([content], { type: 'text/vtt' }); var file = window.URL.createObjectURL(");
                WriteLiteral(@"blob); resolve(file) }); return promise }, isSrt: function (filename) { return filename.split('.').pop().toLowerCase() === 'srt' ? !0 : !1 }, isVTT: function (filename) { return filename.split('.').pop().toLowerCase() === 'vtt' ? !0 : !1 } }; for (var i = 0; i < tracks.length; i++) { subtitle.load(tracks[i]) } }; for (var i = 0; i < videoElements.length; i++) { videoElements[i].addEventListener('loadstart', convertSrtToVtt) } });
    </script>
    <title>Play Video</title>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea7550ea9a324c20f0bbdae56754ba05d4e26c1c7890", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 84 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
     if (Failed)
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <a");
                BeginWriteAttribute("href", " href=\"", 3558, "\"", 3578, 1);
#nullable restore
#line 86 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
WriteAttributeValue("", 3565, RedirectPath, 3565, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">Click here to login</a>\r\n");
#nullable restore
#line 87 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
                WriteLiteral("        <video controls autoplay style=\"width:100%;height:100%;\">\r\n           \r\n            <source");
                BeginWriteAttribute("src", " src=\"", 3728, "\"", 3829, 1);
#nullable restore
#line 92 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
WriteAttributeValue("", 3734, "/OpenVideoStream/" +  RouteData.Values["PFileName"] + HttpContext.Request.QueryString.Value, 3734, 95, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("\r\n                type=\"video/mp4\">\r\n\r\n");
#nullable restore
#line 95 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
             foreach (var item in Subs)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <track");
                BeginWriteAttribute("label", " label=\"", 3947, "\"", 3964, 1);
#nullable restore
#line 97 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
WriteAttributeValue("", 3955, item.Key, 3955, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" kind=\"subtitles\" lang=\"en\"");
                BeginWriteAttribute("src", " src=\"", 3992, "\"", 4035, 4);
                WriteAttributeValue("", 3998, "/OpenStream/", 3998, 12, true);
#nullable restore
#line 97 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
WriteAttributeValue("", 4010, item.Key, 4010, 11, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 4021, "?", 4021, 1, true);
#nullable restore
#line 97 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
WriteAttributeValue("", 4022, item.Value, 4022, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n");
#nullable restore
#line 98 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n            Your browser does not support the video tag.\r\n        </video>\r\n");
#nullable restore
#line 102 "D:\My Projects\C# Git\AirShare\Pages\PlayVideo.cshtml"
    }

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pages_PlayVideo> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_PlayVideo> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_PlayVideo>)PageContext?.ViewData;
        public Pages_PlayVideo Model => ViewData.Model;
    }
}
#pragma warning restore 1591