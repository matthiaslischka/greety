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

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeType)), Times.Once);
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeOtherType)), Times.Once);
        }

        [Fact]
        public void ShouldReturnFoundTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(Sample.Ugly.UglyType))).Returns(new[] {"UglyType"});

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
            private Sample.SomeType _field;
            private Sample.SomeOtherType _otherField;
            private Sample.Ugly.UglyType _uglyField;
#pragma warning restore 169
        }
    }
}