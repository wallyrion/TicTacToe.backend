using System;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace TicTacToe.Common.Tests
{
    public class DateTimeProviderTests
    {
        private readonly IDateTimeProvider _provider;

        public DateTimeProviderTests()
        {
            _provider = Substitute.For<IDateTimeProvider>();
            DateTimeProvider.Provider = _provider;
        }

        [Fact]
        public void UtcNow_Should_Return_Expected_Value()
        {
            var expectedUtcNow = DateTime.UtcNow;

            _provider.UtcNow.Returns(expectedUtcNow);

            var actual = DateTimeProvider.UtcNow;
            actual.Should().Be(expectedUtcNow);

        }
    }
}