#pragma checksum "D:\My Projects\C# Git\AirShare\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54de9042a133d323da3ffcc5bc10af5a837d0d87"
// <auto-generated/>
#pragma warning disable 1591
namespace AirShare.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using AirShare;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using AirShare.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\My Projects\C# Git\AirShare\_Imports.razor"
using AirShare.Pages.Controls;

#line default
#line hidden
#nullable disable
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "ecaps-page-wrapper");
            __builder.AddAttribute(2, "style", "overflow-x:hidden;width: 100vw;/*max-height:100vh;min-height:100vh;overflow-y:auto;background-image:url(img/logomrc.png);background-repeat:no-repeat;background-position:center;*/");
            __builder.AddMarkupContent(3, "\r\n    \r\n    ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "ecaps-page-content");
            __builder.AddAttribute(6, "style", "margin-left:0px;background-color:#fff !important");
            __builder.AddMarkupContent(7, "\r\n    \r\n    ");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "main-content");
            __builder.AddAttribute(10, "style", "width:100vw;padding:0px !important;");
            __builder.AddMarkupContent(11, "\r\n        ");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "style", "overflow-y:scroll;");
            __builder.AddMarkupContent(14, "\r\n            ");
            __builder.AddContent(15, 
#nullable restore
#line 10 "D:\My Projects\C# Git\AirShare\Shared\MainLayout.razor"
             Body

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(16, "\r\n        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(17, "\r\n        ");
            __builder.OpenComponent<AirShare.Pages.Controls.Footer>(18);
            __builder.CloseComponent();
            __builder.AddMarkupContent(19, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\r\n");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
