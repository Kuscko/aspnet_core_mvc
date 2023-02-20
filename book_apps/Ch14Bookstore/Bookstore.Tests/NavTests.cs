using Xunit;
using Bookstore.Models;

namespace Bookstore.Tests
{
    public class NavTests
    {
        [Fact]
        public void ActiveMethod_ReturnsAString()
        {
            string s1 = "Home";               // arrange
            string s2 = "Books";

            var result = Nav.Active(s1, s2);  // act

            Assert.IsType<string>(result);    // assert
        }

        [Theory]
        [InlineData("Home", "Home")]
        [InlineData("Books", "Books")]
        public void ActiveMethod_ReturnsActiveStringIfMatch(string s1, string s2)
        {
            string expected = "active";       // arrange

            var result = Nav.Active(s1, s2);  // act

            Assert.Equal(expected, result);   // assert
        }

        [Theory]
        [InlineData("Home", "Books")]    // runs a test with these arguments
        [InlineData("Books", "books")]   // runs another test with these arguments
        public void ActiveMethod_ReturnsEmptyStringIfNoMatch(string s1, string s2)
        {
            // act
            string active = Nav.Active(s1, s2);

            // assert
            Assert.Equal("", active);
        }
    }
}