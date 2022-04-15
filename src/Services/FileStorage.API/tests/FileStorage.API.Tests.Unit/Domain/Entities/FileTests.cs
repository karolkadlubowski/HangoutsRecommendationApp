using FileStorage.API.Tests.Unit.Utilities.Models;
using FluentAssertions;
using NUnit.Framework;

namespace FileStorage.API.Tests.Unit.Domain.Entities
{
    [TestFixture]
    public class FileTests
    {
        [Test]
        public void SetUrl_WhenCalled_SetFileUrlWithCombinedUrl()
        {
            //Arrange
            const string Key = nameof(Key);
            const string ExpectedUrl = "http://localhost/Key";

            var file = new TestFile(Key);

            //Act
            file.SetUrl("http://localhost");

            //Assert
            file.FileUrl.Should().Be(ExpectedUrl);
        }
    }
}