#pragma checksum "D:\My Projects\C# Git\AirShare\Pages\Controls\MessageBox.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e743f005848803fcb849d9020761f6f598cb167c"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace AirShare.Pages.Controls
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
    public partial class MessageBox : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 25 "D:\My Projects\C# Git\AirShare\Pages\Controls\MessageBox.razor"
       
    System.Timers.Timer T;
    private bool Showing = false;
    protected override void OnInitialized()
    {
        base.OnInitialized();
        T = new System.Timers.Timer();
        T.Enabled = true;
        T.Interval = 100;
        T.Elapsed += Check;

    }
    void Check(Object source, System.Timers.ElapsedEventArgs e)
    {
        if (Core.MsgBox != null && !Showing)
        {
            Showing = true;
            StateHasChanged();
        }
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime js { get; set; }
    }
}
#pragma warning restore 1591
