using FluentAssertions;
using Sample.Nice;
using Sample.Ugly;
using Xunit;

namespace DepChecker.Tests
{
    public class ConstructorDependencyCheckerTests
    {
        private readonly ConstructorDependencyChecker _checker;

        public ConstructorDependencyCheckerTests()
        {
            _checker = new ConstructorDependencyChecker("Sample.Nice");
        }

        [Fact]
        public void ShouldReportAParameterFromOutsideTheHappyZone()
        {
            var errors = _checker.Check<ClassWithUglyConstructorParameter>();
            errors.Should().Contain(err => err.NonHappyZoneTypeName.Contains("UglyType"));
        }
    }
}

namespace Sample
{
    namespace Nice
    {
        class ClassWithUglyConstructorParameter
        {
            // ReSharper disable once UnusedParameter.Local
            public ClassWithUglyConstructorParameter(UglyType ugly) { }
        }
    }

    namespace Ugly
    {
        class UglyType { }
    }
}