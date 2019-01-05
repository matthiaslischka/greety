using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using Moq;
using Xunit;

namespace DepChecker.Tests
{
    public class PropertyDependencyCheckerTests
    {
        private readonly PropertyDependencyChecker _checker;
        private readonly Mock<INamespaceChecker> _namespaceCheckerMock;

        public PropertyDependencyCheckerTests()
        {
            _namespaceCheckerMock = new Mock<INamespaceChecker>();
            _checker = new PropertyDependencyChecker(_namespaceCheckerMock.Object);
        }

        [Fact]
        public void ShouldCheckAllFields()
        {
            _checker.Check(typeof(ClassWithSeveralProperties).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeType)), Times.Once);
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeOtherType)), Times.Once);
        }

        [Fact]
        public void ShouldReturnFoundTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(Sample.Ugly.UglyType))).Returns(new[] { "Sample.Ugly.UglyType" });

            var errors = _checker.Check(typeof(ClassWithSeveralProperties).GetTypeInfo());

            errors.Should().Contain(PropertyDependencyError("UglyProperty", "UglyType"));
        }

        private Expression<Func<IDependencyError, bool>> PropertyDependencyError(string uglyPropertyName, string uglyTypeName)
        {
            return err => err is PropertyDependencyChecker.PropertyDependencyError &&
                          err.ElementName == uglyPropertyName &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }

        private class ClassWithSeveralProperties
        {
            private Sample.SomeType Property { get; }
            private Sample.SomeOtherType OtherProperty { get; }
            private Sample.Ugly.UglyType UglyProperty { get; }
        }
    }
}