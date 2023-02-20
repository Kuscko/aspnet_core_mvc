using System;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Bookstore.Models;

namespace Bookstore.Tests
{
    public class CartTests
    {
        private Cart GetCart()
        {
            // create accessor
            var accessor = new Mock<IHttpContextAccessor>();

            // set up cookies
            var context = new DefaultHttpContext();
            accessor.Setup(m => m.HttpContext)
                .Returns(context);
            accessor.Setup(m => m.HttpContext.Request)
                .Returns(context.Request);
            accessor.Setup(m => m.HttpContext.Response)
                .Returns(context.Response);
            accessor.Setup(m => m.HttpContext.Request.Cookies)
                .Returns(context.Request.Cookies);
            accessor.Setup(m => m.HttpContext.Response.Cookies)
                .Returns(context.Response.Cookies);

            // set up session state
            var session = new Mock<ISession>();
            accessor.Setup(m => m.HttpContext.Session)
                .Returns(session.Object);

            return new Cart(accessor.Object);
        }

        [Fact]
        public void Subtotal_ReturnsADouble()
        {
            // arrange
            Cart cart = GetCart();
            cart.Add(new CartItem { Book = new BookDTO() });

            // act
            var result = cart.Subtotal;

            // assert
            Assert.IsType<double>(result);
        }

        [Theory]
        [InlineData(9.99, 6.89, 12.99)]
        [InlineData(8.97, 45.00, 9.99, 15.00)]
        public void
        Subtotal_ReturnsCorrectCalculation(params double[] prices)
        {
            // arrange
            Cart cart = GetCart();
            for (int i = 0; i < prices.Length; i++)
            {
                var item = new CartItem
                {
                    Book = new BookDTO { BookId = i, Price = prices[i] },
                    Quantity = 1
                };
                cart.Add(item);
            }
            double expected = prices.Sum();

            // act
            var result = cart.Subtotal;

            // assert
            Assert.Equal(Math.Round(expected, 2), Math.Round(result, 2));
        }
    }
}