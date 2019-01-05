using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace DepChecker.Tests
{
    public class DependencyCheckerTests
    {
        [Theory]
        [InlineData(typeof(ConstructorDependencyChecker))]
        [InlineData(typeof(FieldDependencyChecker))]
        [InlineData(typeof(PropertyDependencyChecker))]
        [InlineData(typeof(MethodDependencyChecker))]
        public void ShouldUseSubChecker(Type checkerType)
        {
            var dependencyChecker = DependencyChecker.Create("Sample.HappyZone");

            dependencyChecker.Checkers.Should().Contain(c => c.GetType() == checkerType);
        }

        [Fact]
        public void SmokeTest()
        {
            var checker = DependencyChecker.Create("Sample.HappyZone");

            checker.Check(typeof(string).GetTypeInfo());
        }
    }
}