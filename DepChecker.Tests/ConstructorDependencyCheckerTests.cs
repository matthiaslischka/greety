using System;
using System.Linq.Expressions;
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
            errors.Should().Contain(ConstructorDependencyError("uglyParameter", "UglyType"));
        }

        private Expression<Func<IDependencyError, bool>> ConstructorDependencyError(string uglyParameterName, string uglyTypeName)
        {
            return err => err is ConstructorDependencyChecker.ConstructorParameterDependencyError &&
                          err.ElementName == uglyParameterName &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
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
            public ClassWithUglyConstructorParameter(UglyType uglyParameter) { }
        }
    }
}