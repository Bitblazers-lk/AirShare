#pragma checksum "D:\My Projects\C# Git\AirShare\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "74a9203f9edad95c8f615188ac24cedca5cd366e"
// <auto-generated/>
#pragma warning disable 1591
namespace AirShare.Pages
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
#nullable restore
#line 2 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
using AirShare;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 9 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
 if(ShowUI)
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "card mb-30 border");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "card-body pb-0");
            __builder.AddMarkupContent(4, "<div class=\"card-header border-none bg-transparent d-flex align-items-center justify-content-between p-0 mb-30\"><div class=\"widgets-card-title\"><h5 class=\"card-title mb-0\">♦ Login</h5></div></div>");
#nullable restore
#line 19 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
         if (!string.IsNullOrWhiteSpace(msg))
        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "alert alert-danger");
            __builder.AddContent(7, 
#nullable restore
#line 21 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
                                             msg

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 22 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "form-group mb-3");
            __builder.AddMarkupContent(10, "<label>Username</label>\r\n            ");
            __builder.OpenElement(11, "input");
            __builder.AddAttribute(12, "type", "text");
            __builder.AddAttribute(13, "class", "form-control");
            __builder.AddAttribute(14, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 25 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
                                                           TxtName

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(15, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => TxtName = __value, TxtName));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n        ");
            __builder.OpenElement(17, "div");
            __builder.AddAttribute(18, "class", "form-group mb-3");
            __builder.AddMarkupContent(19, "<label>Password</label>\r\n            ");
            __builder.OpenElement(20, "input");
            __builder.AddAttribute(21, "type", "password");
            __builder.AddAttribute(22, "class", "form-control");
            __builder.AddAttribute(23, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 29 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
                                                               TxtPass

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(24, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => TxtPass = __value, TxtPass));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n        ");
            __builder.OpenElement(26, "div");
            __builder.AddAttribute(27, "class", "form-group mb-3");
            __builder.OpenElement(28, "a");
            __builder.AddAttribute(29, "class", "btn btn-rounded btn-success text-white");
            __builder.AddAttribute(30, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 32 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
                                                                        (() => Login())

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(31, "style", "width:100%");
            __builder.AddContent(32, "Login");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(33, "\r\n        ");
            __builder.OpenElement(34, "div");
            __builder.AddAttribute(35, "class", "form-group mb-3");
            __builder.OpenElement(36, "a");
            __builder.AddAttribute(37, "class", "btn btn-rounded btn-primary text-white");
            __builder.AddAttribute(38, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 35 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
                                                                        (() => LoginAsGuest())

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(39, "style", "width:100%");
            __builder.AddContent(40, "Login as Guest");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 39 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(41, "<h3> Just a sec... </h3>");
#nullable restore
#line 43 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 46 "D:\My Projects\C# Git\AirShare\Pages\Index.razor"
 
    string msg { get; set; }
    string TxtName { get; set; }
    string TxtPass { get; set; }

    bool ShowUI {get; set;}

    [Parameter]
    public string Path { get; set; }

    protected override void OnInitialized()
    {
        ShowUI = true;
        if (Path == null)
        {
            try
            {
                string q = new Uri(NavMan.Uri).Query.ToString().TrimStart('?');
                Path = Uri.UnescapeDataString(q.TrimStart('?')).Trim('"');
                if (Path.Length == 0) Path = null;

                ShowUI = false;
            }
            catch (Exception)
            {
                Path = null;
            }
        }
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            string ck = await JSRuntime.InvokeAsync<string>("GetCookie", "ut");
            if (ck != null)
            {
               
                User cu = Core.AuthToken(ck);
                if (cu != null)
                {
                    Core.Log($"Success : Auth Token cookie {ck}");
                    await Go(cu);
                    return;
                }
                else
                {
                    Core.Log($"Failed : Auth Token cookie {ck}");
                    await JSRuntime.InvokeVoidAsync("EraseCookie", "ut");
                }

            }

            ShowUI = true;
            StateHasChanged();
        }
    }

    public async void Login()
    {
        User usr = Core.Auth(TxtName, TxtPass);
        await Go(usr);

    }

    public async void LoginAsGuest()
    {
        User usr = Core.AddGuest();
        await Go(usr);

    }

    public async Task Go(User usr)
    {
        if (usr != null)
        {
            Userdata.Token = usr.Token();
            await JSRuntime.InvokeVoidAsync("SetCookie", "ut", Userdata.Token);
            msg = "Success " + usr.Name + " " + await JSRuntime.InvokeAsync<string>("GetCookie", "ut");

            if (Path == null)
            {
                string fp = "\\";

                if (!usr.Validate(fp))
                {
                    if (usr.Allowed.Length != 0)
                    {
                        fp = usr.Allowed[0];
                    }
                    else
                    {
                        fp = Core.CreateAirSharedDir();
                    }
                }

                Path = "explorer?" + Uri.EscapeDataString(fp);
            }

            

            Core.Log($"Logged in {usr.Name} as {usr.Lvl} user");

            NavMan.NavigateTo(Path, false);
        }
        else
        {
            msg = "Wrong user name / password";
        }

    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private UserData Userdata { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.AspNetCore.Components.NavigationManager NavMan { get; set; }
    }
}
#pragma warning restore 1591
