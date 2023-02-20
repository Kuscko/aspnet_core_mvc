using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bookstore.TagHelpers
{
    // applies to input or button elements of type submit,
    // and to <a> elements with a 'my-button' attribute
    [HtmlTargetElement(Attributes = "[type=submit]")]
    [HtmlTargetElement("a", Attributes = "my-button")]
    public class ButtonTagHelper : TagHelper
    {
        public bool MyButton { get; set; }  // not used - keeps attribute from being output

        public override void Process(TagHelperContext context, 
        TagHelperOutput output)
        {
            output.Attributes.AppendCssClass("btn btn-primary");
        }
    }

}
