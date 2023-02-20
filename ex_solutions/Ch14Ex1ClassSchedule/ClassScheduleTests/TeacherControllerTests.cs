using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;
using ClassSchedule.Controllers;

namespace ClassScheduleTests
{
    public class TeacherControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {
            // arrange
            var rep = new Mock<IRepository<Teacher>>();
            var controller = new TeacherController(rep.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<ViewResult>(result);

        }
    }
}
