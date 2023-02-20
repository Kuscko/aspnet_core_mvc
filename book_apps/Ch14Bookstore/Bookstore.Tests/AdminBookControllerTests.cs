using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;
using Moq;
using Bookstore.Areas.Admin.Controllers;
using Bookstore.Models;

namespace Bookstore.Tests
{
    public class AdminBookControllerTests
    {
        public IBookstoreUnitOfWork GetUnitOfWork()
        {
            // set up Book repository
            var bookRep = new Mock<IRepository<Book>>();
            bookRep.Setup(m => m.Get(It.IsAny<QueryOptions<Book>>()))
                .Returns(new Book { BookAuthors = new List<BookAuthor>() });
            bookRep.Setup(m => m.List(It.IsAny<QueryOptions<Book>>()))
               .Returns(new List<Book>());
            bookRep.Setup(m => m.Count).Returns(0);

            // set up Author repository
            var authorRep = new Mock<IRepository<Author>>();
            authorRep.Setup(m => m.List(It.IsAny<QueryOptions<Author>>()))
                .Returns(new List<Author>());

            // set up Genre repository
            var genreRep = new Mock<IRepository<Genre>>();
            genreRep.Setup(m => m.List(It.IsAny<QueryOptions<Genre>>()))
                .Returns(new List<Genre>());

            // set up unit of work
            var unit = new Mock<IBookstoreUnitOfWork>();
            unit.Setup(m => m.Books).Returns(bookRep.Object);
            unit.Setup(m => m.Authors).Returns(authorRep.Object);
            unit.Setup(m => m.Genres).Returns(genreRep.Object);

            return unit.Object;
        }

        [Fact]
        public void Edit_GET_ModelIsBookViewModel()
        {
            // arrange
            var unit = GetUnitOfWork();
            var controller = new BookController(unit);

            // act
            var model = controller.Edit(1).ViewData.Model as BookViewModel;

            // assert
            Assert.IsType<BookViewModel>(model);
        }

        [Fact]
        public void Edit_POST_ReturnsViewResultIfModelIsNotValid()
        {
            // arrange
            var unit = GetUnitOfWork();
            var controller = new BookController(unit);

            controller.ModelState.AddModelError("", "Test error message.");
            BookViewModel vm = new BookViewModel();

            // act
            var result = controller.Edit(vm);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_POST_ReturnsRedirectToActionResultIfModelIsValid()
        {
            // arrange
            var unit = GetUnitOfWork();
            var controller = new BookController(unit);
            var temp = new Mock<ITempDataDictionary>();
            controller.TempData = temp.Object;
            BookViewModel vm = new BookViewModel { Book = new Book() };

            // act
            var result = controller.Edit(vm);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}