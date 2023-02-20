using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Bookstore.Controllers;
using Bookstore.Models;

namespace Bookstore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsViewResult()
        {
            // FakeBookRepository - not used
            // arrange
            /*
            var rep = new FakeBookRepository();
            var controller = new HomeController(rep);
            */

            // Moq
            // arrange
            var rep = new Mock<IRepository<Book>>();
            var controller = new HomeController(rep.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexActionMethod_ModelIsABookObject()
        {
            // arrange
            var rep = new FakeBookRepository();
            var controller = new HomeController(rep);

            // act
            var model = controller.Index().ViewData.Model as Book;

            // assert
            Assert.IsType<Book>(model);
        }
    }
}