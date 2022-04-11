using AccountDefinition.API.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace AccountDefinition.API.Tests.Unit.Domain.Entities
{
    [TestFixture]
    public class AccountProviderTests
    {
        [Test]
        [TestCase(" Provider")]
        [TestCase("Provider ")]
        [TestCase(" Provider ")]
        [TestCase("Provider")]
        public void Create_WhenCalled_ReturnAccountProviderWithTrimmedAndUpperCaseProvider(string provider)
        {
            //Arrange
            var expectedProvider = provider.Trim().ToUpperInvariant();

            //Act
            var accountProvider = AccountProvider.Create(provider);

            //Assert
            using (new AssertionScope())
            {
                accountProvider.Provider.Should().Be(expectedProvider);
                accountProvider.CreatedOn.Should().NotBe(default);
                accountProvider.ModifiedOn.Should().BeNull();
            }
        }
    }
}