using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type
using Microsoft.AspNetCore.Routing;             // LinkGenerator
using Bookstore.Models;

namespace Bookstore.TagHelpers
{
    [HtmlTargetElement("my-sorting-link")]
    public class SortingLinkTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public SortingLinkTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public RouteDictionary Current { get; set; }
        public string SortField { get; set; }

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            var routes = Current.Clone();
            routes.SetSortAndDirection(SortField, Current);

            string ctlr = ViewCtx.RouteData.Values["controller"].ToString();
            string action = ViewCtx.RouteData.Values["action"].ToString();
            string url = linkBuilder.GetPathByAction(action, ctlr, routes);

            output.BuildLink(url, "text-white");
        }
    }
}
