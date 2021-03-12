#pragma checksum "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\Account\ChangeConfirm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8fff437168ba60018ac55c0e85272e00412be1c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ChangeConfirm), @"mvc.1.0.view", @"/Views/Account/ChangeConfirm.cshtml")]
namespace AspNetCore
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
#line 1 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Models.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Models.Food;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Models.Pagination;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Models.Role;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Models.User;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\_ViewImports.cshtml"
using LunchOrderManagement.Models.Order;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8fff437168ba60018ac55c0e85272e00412be1c2", @"/Views/Account/ChangeConfirm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f2043e95ba3a1605196d409051504d70b07b087e", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ChangeConfirm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-uppercase btn btn--radius btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\Account\ChangeConfirm.cshtml"
  
    AppIdentityUser user = await userManager.GetUserAsync(User);
    ViewBag.Title = "Change success";
    string name = $"{user.FirstName} {user.LastName}".ToUpper();

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""wrapper container"">
    <div class=""inner"">
        <div class=""row"">
            <div class=""mt-3 col-12 text-center""><i class=""far fa-check-square"" style=""font-size:50px; color: green;""></i></div>
            <div class=""mt-3 mb-3 col-12"">
                <h3 class=""text-center text-primary font-weight-bold"">
                    <strong>
                        ");
#nullable restore
#line 15 "D:\LEARNING\MODULE 3\PROJECTS\LunchOrderManagement\LunchOrderManagement\Views\Account\ChangeConfirm.cshtml"
                   Write(name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" WAS SUCCESSFUL CONFIRM FOR ACTION TO CHANGE YOUR INFORMATION\r\n                    </strong>\r\n                </h3>\r\n            </div>\r\n            <div class=\"col-12 m-auto text-center\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8fff437168ba60018ac55c0e85272e00412be1c26958", async() => {
                WriteLiteral("Go TO Homepage");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<AppIdentityUser> userManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591