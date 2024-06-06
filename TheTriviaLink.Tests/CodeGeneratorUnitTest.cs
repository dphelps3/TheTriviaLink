using Xunit;
using Moq;
using TriviaApp;
using TriviaLink.Services;

namespace TheTriviaLink.Tests
{
    public class CodeGeneratorUnitTest
    {
        [Fact]
        public async Task GenerateUniqueCode_ReturnsFourLetterString()
        {
            // Arrange
            var mockCodeGeneratorService = new Mock<ICodeGeneratorService>();
            mockCodeGeneratorService.Setup(service => service.GenerateUniqueCode()).ReturnsAsync("1234");

            // Act
            string result = await mockCodeGeneratorService.Object.GenerateUniqueCode();

            // Assert
            Assert.Equal(4, result.Length);
            Assert.Matches("^[A-Za-z]{4}$", result);
        }
    }
}
