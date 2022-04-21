using FileStorage.API.Domain.Factories;
using FluentAssertions;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Domain.Factories
{
    [TestFixture]
    public class FileUrlFactoryTests
    {
        [Test]
        [TestCase("test1", "http://localhost/test1")]
        [TestCase("test1/test2", "http://localhost/test1/test2")]
        [TestCase("test1\\test2", "http://localhost/test1/test2")]
        public void Prepare_WhenCalled_ReturnProperUrl(string fileKey, string expectedResult)
        {
            //Arrange
            const string BaseUrl = "http://localhost";

            //Act
            var result = FileUrlFactory.Prepare(BaseUrl, fileKey);

            //Assert
            result.Should().Be(expectedResult);
        }
    }
}