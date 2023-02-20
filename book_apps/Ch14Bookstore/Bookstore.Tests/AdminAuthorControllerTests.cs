using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;
using Moq;
using Bookstore.Models;
using Bookstore.Areas.Admin.Controllers;

namespace Bookstore.Tests
{
    public class AdminAuthorControllerTests
    {
        [Fact]
        public void Edit_GET_ModelIsAuthorObject()
        {
            // arrange
            var rep = new Mock<IRepository<Author>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Author());
            var controller = new AuthorController(rep.Object);

            // act
            var model = controller.Edit(1).ViewData.Model as Author;

            // assert
            Assert.IsType<Author>(model);
        }

        [Fact]
        public void Edit_POST_ReturnsRedirectToActionResultIfModelStateIsValid()
        {
            // FakeAuthorRepository and FakeTempData classes - not used
            // arrange
            /*
            var rep = new FakeAuthorRepository();
            var controller = new AuthorController(rep)
            {
                TempData = new FakeTempData()
            };
            */

            // Moq
            // arrange
            var rep = new Mock<IRepository<Author>>();            
            var temp = new Mock<ITempDataDictionary>();
            var controller = new AuthorController(rep.Object)
            {
                TempData = temp.Object
            };

            // act
            var result = controller.Edit(new Author());

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Edit_POST_ReturnsViewResultIfModelStateIsNotValid()
        {
            // arrange
            var rep = new Mock<IRepository<Author>>();
            var controller = new AuthorController(rep.Object);
            controller.ModelState.AddModelError("", "Test error message.");

            // act
            var result = controller.Edit(new Author());

            // assert
            Assert.IsType<ViewResult>(result);
        }
    }
}