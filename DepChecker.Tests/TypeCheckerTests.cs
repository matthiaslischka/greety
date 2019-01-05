using System;
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
            _checker = new TypeChecker();
        }

        [Fact]
        public void ShouldNotFindSystemTypes()
        {
            var uglyTypeNames = _checker.CheckType(typeof(string));
            uglyTypeNames.Should().BeEmpty();
        }

        [Fact]
        public void ShouldNotFindTypesInLegalNamespace()
        {
            _checker.AddLegalNamespace("Sample.Legal");
            var uglyTypeNames = _checker.CheckType(typeof(Sample.Legal.LegalClass));
            uglyTypeNames.Should().BeEmpty();
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

        [Fact]
        public void ShouldFindATypeFromOutsideTheHappyZoneUsedInAGenericTypeTwoStagesDeep()
        {
            var errors = _checker.CheckType(typeof(Func<IEnumerable<UglyType>>));
            errors.Should().Contain(name => name.EndsWith("UglyType"));
        }
    }
}

namespace Sample.Legal
{
    // ReSharper disable once UnusedMember.Global
    class LegalClass { }
}