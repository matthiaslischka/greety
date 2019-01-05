using System;
using FluentAssertions;
using Xunit;

namespace DepChecker.Tests
{
    public class DependencyCheckerTests
    {
        [Theory]
        [InlineData(typeof(ConstructorDependencyChecker))]
        [InlineData(typeof(FieldDependencyChecker))]
        public void ShouldUseSubChecker(Type checkerType)
        {
            var dependencyChecker = DependencyChecker.Create("Sample.HappyZone");

            dependencyChecker.Checkers.Should().Contain(c => c.GetType() == checkerType);
        }
    }
}