using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // [ViewContext] attribute
using Microsoft.AspNetCore.Mvc.Rendering;       // ViewContext data type
using Microsoft.AspNetCore.Routing;             // LinkGenerator

namespace ClassSchedule.TagHelpers
{
    public class MyLinkButtonTagHelper : TagHelper
    {
        private LinkGenerator linkBuilder;
        public MyLinkButtonTagHelper(LinkGenerator lg) => linkBuilder = lg;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public string Action { get; set; }
        public string Controller { get; set; }
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string action = Action ?? ViewCtx.RouteData.Values["action"].ToString();
            string controller = Controller ?? ViewCtx.RouteData.Values["controller"].ToString();
            var segment = (string.IsNullOrEmpty(Id)) ? null : new { Id };

            string url = linkBuilder.GetPathByAction(action, controller, segment);
            string css = (IsActive) ? "btn btn-dark" : "btn btn-outline-dark";

            output.BuildLink(url, css);
        }
    }
}
