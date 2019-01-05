using System;
using System.Linq.Expressions;
using FluentAssertions;
using Sample.Nice;
using Sample.Ugly;
using Xunit;

namespace DepChecker.Tests
{
    public class FieldDependencyCheckerTests
    {
        private readonly FieldDependencyChecker _checker;

        public FieldDependencyCheckerTests()
        {
            _checker = new FieldDependencyChecker("Sample.Nice");
        }

        [Fact]
        public void ShouldReportAParameterFromOutsideTheHappyZone()
        {
            var errors = _checker.Check<ClassWithUglyField>();
            errors.Should().Contain(FieldDependencyError("_uglyField", "UglyType"));
        }

        private Expression<Func<IDependencyError, bool>> FieldDependencyError(string uglyFieldName, string uglyTypeName)
        {
            return err => err is FieldDependencyChecker.FieldDependencyError &&
                          err.ElementName == uglyFieldName &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }
    }
}

namespace Sample
{
    namespace Nice
    {
        // ReSharper disable once ClassNeverInstantiated.Global
        class ClassWithUglyField
        {
            private UglyType _uglyField;
        }
    }
}