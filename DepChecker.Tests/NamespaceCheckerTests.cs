using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DepChecker.Tests
{
    public class NamespaceCheckerTests
    {
        private readonly NamespaceChecker _checker;

        public NamespaceCheckerTests()
        {
            _checker = new NamespaceChecker();
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
            _checker.AddLegalNamespace("SampleTypes.Legal");
            var uglyTypeNames = _checker.CheckType(typeof(SampleTypes.Legal.LegalType));
            uglyTypeNames.Should().BeEmpty();
        }

        [Fact]
        public void ShouldFindATypeFromOutsideTheHappyZone()
        {
            var uglyTypeNames = _checker.CheckType(typeof(SampleTypes.Ugly.UglyType));
            uglyTypeNames.Should().Contain(name => name.EndsWith("UglyType"));
        }

        [Fact]
        public void ShouldFindATypeFromOutsideTheHappyZoneUsedInAGenericType()
        {
            var errors = _checker.CheckType(typeof(IEnumerable<SampleTypes.Ugly.UglyType>));
            errors.Should().Contain(name => name.EndsWith("UglyType"));
        }

        [Fact]
        public void ShouldFindATypeFromOutsideTheHappyZoneUsedInAGenericTypeTwoStagesDeep()
        {
            var errors = _checker.CheckType(typeof(Func<IEnumerable<SampleTypes.Ugly.UglyType>>));
            errors.Should().Contain(name => name.EndsWith("UglyType"));
        }
    }
}