#pragma checksum "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8912dc2679951f42b7d6e15192f5f277bbb2095d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Invoice_InsertPrintInvoices), @"mvc.1.0.view", @"/Views/Invoice/InsertPrintInvoices.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Invoice/InsertPrintInvoices.cshtml", typeof(AspNetCore.Views_Invoice_InsertPrintInvoices))]
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
#line 1 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\_ViewImports.cshtml"
using WebApplication3;

#line default
#line hidden
#line 2 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\_ViewImports.cshtml"
using WebApplication3.Models;

#line default
#line hidden
#line 3 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\_ViewImports.cshtml"
using WebApplication3.Models.Product;

#line default
#line hidden
#line 2 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8912dc2679951f42b7d6e15192f5f277bbb2095d", @"/Views/Invoice/InsertPrintInvoices.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01ab8e92108ad63c31d1c5f8b571650ef3360518", @"/Views/_ViewImports.cshtml")]
    public class Views_Invoice_InsertPrintInvoices : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(93, 119, true);
            WriteLiteral("\r\n<div id=\"main\" class=\"templatemo-content col-1 light-gray-bg\">\r\n    <div style=\"margin:100px;padding-top:80px\">\r\n\r\n\r\n");
            EndContext();
#line 9 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml"
          
            ViewData["Title"] = "InsertPrintInvoices";
        

#line default
#line hidden
            BeginContext(291, 30, true);
            WriteLiteral("\r\n        <h2>Faktury</h2>\r\n\r\n");
            EndContext();
#line 15 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml"
         using (Html.BeginForm("PrintInvoice", "Invoice"))
        {

#line default
#line hidden
            BeginContext(392, 104, true);
            WriteLiteral("            <b>\r\n                Numer faktury/numer zamówienia:\r\n            </b>\r\n            <br />\r\n");
            EndContext();
            BeginContext(509, 51, false);
#line 21 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml"
       Write(Html.RadioButton("SearchBy", "InvoiceNumber", true));

#line default
#line hidden
            EndContext();
            BeginContext(567, 13, true);
            WriteLiteral("Numer faktury");
            EndContext();
            BeginContext(587, 22, true);
            WriteLiteral("\r\n            <br />\r\n");
            EndContext();
            BeginContext(622, 43, false);
#line 23 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml"
       Write(Html.RadioButton("SearchBy", "OrderNumber"));

#line default
#line hidden
            EndContext();
            BeginContext(672, 16, true);
            WriteLiteral("Numer zamówienia");
            EndContext();
            BeginContext(695, 152, true);
            WriteLiteral("\r\n            <br />\r\n            <b>\r\n                Numer:\r\n            </b>\r\n            <input id=\"OrderNumber\" name=\"OrderNumber\" type=\"text\" />\r\n");
            EndContext();
            BeginContext(849, 20, true);
            WriteLiteral("            <br />\r\n");
            EndContext();
            BeginContext(882, 40, false);
#line 31 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml"
       Write(Html.RadioButton("email", "admin", true));

#line default
#line hidden
            EndContext();
            BeginContext(924, 55, true);
            WriteLiteral("            <input type=\"submit\" value=\"Potwierdź\" />\r\n");
            EndContext();
#line 33 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Invoice\InsertPrintInvoices.cshtml"
        }

#line default
#line hidden
            BeginContext(990, 29, true);
            WriteLiteral("\r\n        <div>\r\n            ");
            EndContext();
            BeginContext(1019, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a40f0d6a8924138a95acbb4c574c6c1", async() => {
                BeginContext(1063, 6, true);
                WriteLiteral("Powrót");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1073, 36, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<IdentityUser> SignInManager { get; private set; }
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
