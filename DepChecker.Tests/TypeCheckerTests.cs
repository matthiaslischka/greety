using System.Collections.Generic;
using FluentAssertions;
using Sample.Ugly;
using Xunit;

namespace DepChecker.Tests
{
    public class TypeCheckerTests
    {
        private readonly TypeChecker _checker;

        public TypeCheckerTests()
        {
            _checker = new TypeChecker("Sample.Nice");
        }

        [Fact]
        public void ShouldFindATypeFromOutsideTheHappyZone()
        {
            var uglyTypeNames = _checker.CheckType(typeof(UglyType));
            uglyTypeNames.Should().Contain(name => name.EndsWith("UglyType"));
        }

        [Fact]
        public void ShouldFindATypeFromOutsideTheHappyZoneUsedInAGenericType()
        {
            var errors = _checker.CheckType(typeof(IEnumerable<UglyType>));
            errors.Should().Contain(name => name.EndsWith("UglyType"));
        }
    }
}