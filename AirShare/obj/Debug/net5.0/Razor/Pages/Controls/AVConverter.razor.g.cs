#pragma checksum "D:\My Projects\C# Git\AirShare\AirShare\Pages\Controls\AVConverter.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "da95ec6da751d9227f04cc1af3dcaa1cda6ffac2"
// <auto-generated/>
#pragma warning disable 1591
namespace AirShare.Pages.Controls
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using AirShare;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using AirShare.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\My Projects\C# Git\AirShare\AirShare\_Imports.razor"
using AirShare.Pages.Controls;

#line default
#line hidden
#nullable disable
    public partial class AVConverter : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "height-card box-margin");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "card");
            __builder.AddMarkupContent(4, "<video class=\"card-img-top\" controls></video>\r\n\r\n        ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "card-body text-center");
            __builder.AddMarkupContent(7, "<h5 class=\"mt-20\"><a class=\"text-dark\">INNA - Maza Jaja (Official Music Video).mp4</a></h5>\r\n\r\n            ");
            __builder.AddMarkupContent(8, "<p class=\"card-text\">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nisi sint voluptatum nobis mollitia odit eaque quod, velit magni optio quo?</p>\r\n\r\n            ");
            __builder.AddMarkupContent(9, "<p class=\"card-text\"><button class=\"btn btn-sm btn-primary\">Get Info</button></p>\r\n            ");
            __builder.AddMarkupContent(10, @"<div class=""table-responsive""><table class=""file-ex table-borderless table-nowrap""><thead><tr><th>Name</th>
                            <th>SelDirectoryInfo.Name</th></tr>
                        <tr><th>Full Name</th>
                            <th>SelDirectoryInfo.FullName</th></tr>
                        <tr><th>Size</th>
                            <th></th></tr>
                        <tr><th>Created</th>
                            <th>SelDirectoryInfo.CreationTime</th></tr>
                        <tr><th>Last Access</th>
                            <th>SelDirectoryInfo.LastAccessTime</th></tr>
                        <tr><th>Last Write</th>
                            <th>SelDirectoryInfo.LastWriteTime</th></tr>
                        <tr><th>Attributes</th>
                            <th>SelDirectoryInfo.Attributes</th></tr></thead></table></div>
            <hr>

            ");
            __builder.OpenElement(11, "div");
            __builder.AddAttribute(12, "class", "row align-items-center justify-content-between");
            __builder.OpenElement(13, "div");
            __builder.AddAttribute(14, "class", "col-auto");
            __builder.OpenElement(15, "select");
            __builder.AddAttribute(16, "class", "form-control");
            __builder.OpenElement(17, "option");
            __builder.AddContent(18, "Choose Type");
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n                        ");
            __builder.OpenElement(20, "option");
            __builder.AddContent(21, "Extract Audio");
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n                        ");
            __builder.OpenElement(23, "option");
            __builder.AddContent(24, "Download Video");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n                ");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "col-auto");
            __builder.OpenElement(28, "select");
            __builder.AddAttribute(29, "class", "form-control");
            __builder.OpenElement(30, "option");
            __builder.AddContent(31, "Choose Quality");
            __builder.CloseElement();
            __builder.AddMarkupContent(32, "\r\n                        ");
            __builder.OpenElement(33, "option");
            __builder.AddContent(34, "MP3");
            __builder.CloseElement();
            __builder.AddMarkupContent(35, "\r\n                        ");
            __builder.OpenElement(36, "option");
            __builder.AddContent(37, "M4A");
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n                        ");
            __builder.OpenElement(39, "option");
            __builder.AddContent(40, "OGG");
            __builder.CloseElement();
            __builder.AddMarkupContent(41, "\r\n                        ");
            __builder.OpenElement(42, "option");
            __builder.AddContent(43, "Original Quality");
            __builder.CloseElement();
            __builder.AddMarkupContent(44, "\r\n                        ");
            __builder.OpenElement(45, "option");
            __builder.AddContent(46, "1080p");
            __builder.CloseElement();
            __builder.AddMarkupContent(47, "\r\n                        ");
            __builder.OpenElement(48, "option");
            __builder.AddContent(49, "720p");
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n                        ");
            __builder.OpenElement(51, "option");
            __builder.AddContent(52, "640p");
            __builder.CloseElement();
            __builder.AddMarkupContent(53, "\r\n                        ");
            __builder.OpenElement(54, "option");
            __builder.AddContent(55, "360p");
            __builder.CloseElement();
            __builder.AddMarkupContent(56, "\r\n                        ");
            __builder.OpenElement(57, "option");
            __builder.AddContent(58, "144p");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(59, "\r\n                ");
            __builder.AddMarkupContent(60, "<div class=\"col-auto\"><button class=\"btn btn-sm btn-warning\">Convert</button></div>");
            __builder.CloseElement();
            __builder.AddMarkupContent(61, "\r\n            <hr>\r\n\r\n            ");
            __builder.AddMarkupContent(62, @"<div class=""row align-items-center justify-content-between""><div class=""col-12""><div class=""input-group mb-0""><input type=""text"" class=""form-control"" placeholder=""File URL"">
                        <div class=""input-group-append""><button class=""btn btn-secondary"" type=""button""><i class=""fa fa-copy""></i></button>
                            <button class=""btn btn-success active"" type=""button""><i class=""fa fa-download""></i></button></div></div></div></div>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
