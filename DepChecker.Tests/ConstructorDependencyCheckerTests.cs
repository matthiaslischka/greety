using System;
using System.Collections.Generic;
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
            _checker = new ConstructorDependencyChecker(new TypeChecker("Sample.Nice"));
        }

        [Fact]
        public void ShouldReportAParameterFromOutsideTheHappyZone()
        {
            var errors = _checker.Check<ClassWithUglyConstructorParameter>();
            errors.Should().Contain(ConstructorDependencyError("uglyParameter", "UglyType"));
        }

        [Fact]
        public void ShouldReportAParameterFromOutsideTheHappyZoneUsedInAGenericType()
        {
            var errors = _checker.Check<ClassWithUglyConstructorParameterUsedInAGenericType>();
            errors.Should().Contain(ConstructorDependencyError("indirectUglyParameter", "UglyType"));
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
        // ReSharper disable UnusedParameter.Local
        class ClassWithUglyConstructorParameter
        {
            public ClassWithUglyConstructorParameter(UglyType uglyParameter) { }
        }

        class ClassWithUglyConstructorParameterUsedInAGenericType
        {
            public ClassWithUglyConstructorParameterUsedInAGenericType(IEnumerable<UglyType> indirectUglyParameter) { }
        }
        // ReSharper restore UnusedParameter.Local
    }
}