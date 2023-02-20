using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Bookstore.Controllers;
using Bookstore.Models;

namespace Bookstore.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void Index_ReturnsARedirectToActionResult()
        {
            // arrange
            var unit = new Mock<IBookstoreUnitOfWork>();
            var controller = new BookController(unit.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Index_RedirectsToListActionMethod()
        {
            // arrange
            var unit = new Mock<IBookstoreUnitOfWork>();
            var controller = new BookController(unit.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void Details_ModelIsABookObject()
        {
            // arrange
            var bookRep = new Mock<IRepository<Book>>();
            bookRep.Setup(m => m.Get(It.IsAny<QueryOptions<Book>>()))
                .Returns(new Book { BookAuthors = new List<BookAuthor>() });

            var unit = new Mock<IBookstoreUnitOfWork>();
            unit.Setup(m => m.Books).Returns(bookRep.Object);

            var controller = new BookController(unit.Object);

            // act
            var model = controller.Details(1).ViewData.Model as Book;

            // assert
            Assert.IsType<Book>(model);
        }

    }
}