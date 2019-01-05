using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using Moq;
using Xunit;

namespace DepChecker.Tests
{
    public class FieldDependencyCheckerTests
    {
        private readonly FieldDependencyChecker _checker;
        private readonly Mock<INamespaceChecker> _namespaceCheckerMock;

        public FieldDependencyCheckerTests()
        {
            _namespaceCheckerMock = new Mock<INamespaceChecker>();
            _checker = new FieldDependencyChecker(_namespaceCheckerMock.Object);
        }

        [Fact]
        public void ShouldCheckAllFields()
        {
            _checker.Check(typeof(ClassWithSeveralFields).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SampleTypes.SomeType)), Times.Once);
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SampleTypes.SomeOtherType)), Times.Once);
        }

        [Fact]
        public void ShouldReturnFoundTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(SampleTypes.Ugly.UglyType))).Returns(new[] {"UglyType"});

            var errors = _checker.Check(typeof(ClassWithSeveralFields).GetTypeInfo());

            errors.Should().Contain(FieldDependencyError("_uglyField", "UglyType"));
        }

        private Expression<Func<IDependencyError, bool>> FieldDependencyError(string uglyParameterName, string uglyTypeName)
        {
            return err => err is FieldDependencyChecker.FieldDependencyError &&
                          err.ElementName == uglyParameterName &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }

        private class ClassWithSeveralFields
        {
#pragma warning disable 169
            private SampleTypes.SomeType _field;
            private SampleTypes.SomeOtherType _otherField;
            private SampleTypes.Ugly.UglyType _uglyField;
#pragma warning restore 169
        }
    }
}