#pragma checksum "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\Shared\_IndexArticle.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3ae0958405832fe0fc583142d02dda23cfb14be8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__IndexArticle), @"mvc.1.0.view", @"/Views/Shared/_IndexArticle.cshtml")]
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
#line 1 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using HalicHub;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using HalicHub.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using Halic.Entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using HalicHub.ViewComponents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using HalicHub.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\_ViewImports.cshtml"
using HalicHub.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ae0958405832fe0fc583142d02dda23cfb14be8", @"/Views/Shared/_IndexArticle.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1d447f06f600cad35a08585f36a3d6ae0aa3e3e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__IndexArticle : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ArticleListViewModel>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n\n");
#nullable restore
#line 4 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\Shared\_IndexArticle.cshtml"
 foreach (var item in Model.Articles)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"col-lg-12 speaker-wrap ftco-animate d-flex lg-mobil\">\n    <div class=\"row\">\n        <div style=\"text-align: center\" class=\"col-lg-4 article-width\">\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "3ae0958405832fe0fc583142d02dda23cfb14be84737", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 252, "~/img/", 252, 6, true);
#nullable restore
#line 9 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\Shared\_IndexArticle.cshtml"
AddHtmlAttributeValue("", 258, item.Image, 258, 11, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n        </div>\n        <div style=\"margin: auto\" class=\"col-lg-8 article-width\">\n            <h2><a class=\"font-sz\" style=\"font-weight: 700\" href=\"#\">");
#nullable restore
#line 12 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\Shared\_IndexArticle.cshtml"
                                                                Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h2>\n            <p class=\"font-p-sz\">");
#nullable restore
#line 13 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\Shared\_IndexArticle.cshtml"
                            Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\n            <h3 style=\"font-weight: 700\" class=\"speaker-name font-sz\">&mdash; <a href=\"#\">");
#nullable restore
#line 14 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\Shared\_IndexArticle.cshtml"
                                                                                     Write(item.ArticleAuthors);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></h3>\n        </div>\n    </div>\n</div>");
#nullable restore
#line 17 "C:\Users\Ahmet\Desktop\kilit\25.10\HalicHub-main\HalicHub\Views\Shared\_IndexArticle.cshtml"
      }

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ArticleListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
