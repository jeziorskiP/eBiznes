#pragma checksum "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71e737d0da4c9a58ace73c3f984c3a05bf3df750"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Complaint_ShowComplaint), @"mvc.1.0.view", @"/Views/Complaint/ShowComplaint.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Complaint/ShowComplaint.cshtml", typeof(AspNetCore.Views_Complaint_ShowComplaint))]
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
#line 1 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"71e737d0da4c9a58ace73c3f984c3a05bf3df750", @"/Views/Complaint/ShowComplaint.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01ab8e92108ad63c31d1c5f8b571650ef3360518", @"/Views/_ViewImports.cshtml")]
    public class Views_Complaint_ShowComplaint : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApplication3.Models.Complaint.ComplaintListingModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ResolveComplaint", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(91, 117, true);
            WriteLiteral("\r\n<div id=\"main\" class=\"templatemo-content col-1 light-gray-bg\">\r\n\r\n    <div style=\"margin:100px;padding-top:80px\">\r\n");
            EndContext();
            BeginContext(279, 8, true);
            WriteLiteral("        ");
            EndContext();
#line 8 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
          
            ViewData["Title"] = "GetAll";
        

#line default
#line hidden
            BeginContext(345, 577, true);
            WriteLiteral(@"

        <div id=""clients"">
            <h3>Reklamacja</h3>
            <div id=""clientsTable"">
                <table class=""table table-condensed"" id=""catalogIndexTable"">
                    <thead>
                        <tr>
                            <td>Numer zamówienia</td>
                            <td>Data utworzenie </td>
                            <td>Opis</td>
                            <td>Status</td>
                            <td>Rozwiązanie</td>
                        </tr>
                    </thead>
                    <tbody>
");
            EndContext();
#line 27 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                         foreach (var c in Model.Complaints)
                        {

#line default
#line hidden
            BeginContext(1011, 97, true);
            WriteLiteral("                            <tr class=\"clientRow\">\r\n                                <td class=\"\">");
            EndContext();
            BeginContext(1109, 13, false);
#line 30 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                        Write(c.OrderNumber);

#line default
#line hidden
            EndContext();
            BeginContext(1122, 52, true);
            WriteLiteral("</td>\r\n                                <td class=\"\">");
            EndContext();
            BeginContext(1175, 12, false);
#line 31 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                        Write(c.CreateDate);

#line default
#line hidden
            EndContext();
            BeginContext(1187, 52, true);
            WriteLiteral("</td>\r\n                                <td class=\"\">");
            EndContext();
            BeginContext(1240, 13, false);
#line 32 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                        Write(c.Description);

#line default
#line hidden
            EndContext();
            BeginContext(1253, 52, true);
            WriteLiteral("</td>\r\n                                <td class=\"\">");
            EndContext();
            BeginContext(1306, 8, false);
#line 33 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                        Write(c.Status);

#line default
#line hidden
            EndContext();
            BeginContext(1314, 52, true);
            WriteLiteral("</td>\r\n                                <td class=\"\">");
            EndContext();
            BeginContext(1367, 12, false);
#line 34 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                        Write(c.Resolution);

#line default
#line hidden
            EndContext();
            BeginContext(1379, 7, true);
            WriteLiteral("</td>\r\n");
            EndContext();
#line 35 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                 if (SignInManager.IsSignedIn(User))
                                {

#line default
#line hidden
            BeginContext(1491, 82, true);
            WriteLiteral("                                    <td>\r\n                                        ");
            EndContext();
            BeginContext(1573, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6839f1f574b142bda85f2cd3efb1e90a", async() => {
                BeginContext(1636, 7, true);
                WriteLiteral("Rozwiąż");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-ComplaintId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 38 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                                                                    WriteLiteral(c.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["ComplaintId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-ComplaintId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["ComplaintId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1647, 45, true);
            WriteLiteral("\r\n                                    </td>\r\n");
            EndContext();
#line 40 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                                }

#line default
#line hidden
            BeginContext(1727, 39, true);
            WriteLiteral("\r\n\r\n                            </tr>\r\n");
            EndContext();
#line 44 "D:\Sources\VisualStudioRepo\WebApplication3\WebApplication3\Views\Complaint\ShowComplaint.cshtml"
                        }

#line default
#line hidden
            BeginContext(1793, 136, true);
            WriteLiteral("                    </tbody>\r\n                </table>\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n    </div>\r\n\r\n    \r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApplication3.Models.Complaint.ComplaintListingModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
