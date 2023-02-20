using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type

namespace Bookstore.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "[class=nav-link]", ParentTag = "li")]
    public class ActiveNavbarTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        [HtmlAttributeName("my-mark-area-active")]
        public bool IsAreaOnly { get; set; }

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            string area = ViewCtx.RouteData.Values["area"]?.ToString() ?? "";
            string ctlr = ViewCtx.RouteData.Values["controller"]?.ToString();

            string aspArea = context.AllAttributes["asp-area"]?.Value?.ToString() ?? "";
            string aspCtlr = context.AllAttributes["asp-controller"]?.Value?.ToString();

            if (area == aspArea && ctlr == aspCtlr)
                output.Attributes.AppendCssClass("active");
            else if (IsAreaOnly && area == aspArea)
                output.Attributes.AppendCssClass("active");
        }
    }
}
