using Microsoft.AspNetCore.Razor.TagHelpers;
using Bookstore.Models;

namespace Bookstore.TagHelpers
{
    [HtmlTargetElement("span", Attributes = "my-cart-badge")]
    public class CartBadgeTagHelper : TagHelper
    {
        private ICart cart;
        public CartBadgeTagHelper(ICart c) => cart = c;

        public bool MyCartBadge { get; set; } // not used - keeps attribute from being output

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            output.Content.SetContent(cart.Count?.ToString());
        }
    }
}
