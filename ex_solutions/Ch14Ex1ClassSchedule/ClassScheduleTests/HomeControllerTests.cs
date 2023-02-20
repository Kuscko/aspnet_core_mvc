using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ClassSchedule.Models;
using ClassSchedule.Controllers;

namespace ClassScheduleTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {
            // arrange
            var unit = new Mock<IClassScheduleUnitOfWork>();
            var classes = new Mock<IRepository<Class>>();
            var days = new Mock<IRepository<Day>>();
            unit.Setup(r => r.Classes).Returns(classes.Object);
            unit.Setup(r => r.Days).Returns(days.Object);

            var http = new Mock<IHttpContextAccessor>();

            var controller = new HomeController(unit.Object, http.Object);

            // act
            var result = controller.Index(0);

            // assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
