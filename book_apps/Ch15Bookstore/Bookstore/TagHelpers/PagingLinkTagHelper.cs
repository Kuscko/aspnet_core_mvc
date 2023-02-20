using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type
using Microsoft.AspNetCore.Routing;             // LinkGenerator
using Bookstore.Models;

namespace Bookstore.TagHelpers
{
    [HtmlTargetElement("my-paging-link")]
    public class PagingLinkTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public PagingLinkTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public int Number { get; set; }
        public RouteDictionary Current { get; set; }

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            // update routes for this paging link
            var routes = Current.Clone();
            routes.PageNumber = Number;

            // get controller and action method, create paging link URL
            string ctlr = ViewCtx.RouteData.Values["controller"].ToString();
            string action = ViewCtx.RouteData.Values["action"].ToString();
            string url = linkBuilder.GetPathByAction(action, ctlr, routes);

            // build up CSS string
            string linkClasses = "btn btn-outline-primary";
            if (Number == Current.PageNumber)
                linkClasses += " active";

            // create link
            output.BuildLink(url, linkClasses);
            output.Content.SetContent(Number.ToString());
        }
    }
}