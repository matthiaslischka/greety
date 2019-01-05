using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using Moq;
using Xunit;

namespace DepChecker.Tests
{
    public class ConstructorDependencyCheckerTests
    {
        private readonly ConstructorDependencyChecker _checker;
        private readonly Mock<INamespaceChecker> _namespaceCheckerMock;

        public ConstructorDependencyCheckerTests()
        {
            _namespaceCheckerMock = new Mock<INamespaceChecker>();
            _checker = new ConstructorDependencyChecker(_namespaceCheckerMock.Object);
        }

        [Fact]
        public void ShouldCheckAllParametersOfAllConstructors()
        {
            _checker.Check(typeof(ClassWithSeveralConstructors).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SampleTypes.SomeType)), Times.Exactly(2));
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SampleTypes.SomeOtherType)), Times.Once);
        }

        [Fact]
        public void ShouldReturnFoundTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(SampleTypes.Ugly.UglyType))).Returns(new[] { "Sample.Ugly.UglyType" });

            var errors = _checker.Check(typeof(ClassWithSeveralConstructors).GetTypeInfo());

            errors.Should().Contain(ConstructorDependencyError("someUglyParameter", "UglyType"));
        }

        private Expression<Func<IDependencyError, bool>> ConstructorDependencyError(string uglyParameterName, string uglyTypeName)
        {
            return err => err is ConstructorDependencyChecker.ConstructorParameterDependencyError &&
                          err.ElementName == uglyParameterName &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }

        private class ClassWithSeveralConstructors
        {
            // ReSharper disable UnusedParameter.Local
            // ReSharper disable UnusedMember.Local
            public ClassWithSeveralConstructors(SampleTypes.SomeType someParameter) { }
            public ClassWithSeveralConstructors(SampleTypes.Ugly.UglyType someUglyParameter) { }
            public ClassWithSeveralConstructors(SampleTypes.SomeType someParameter, SampleTypes.SomeOtherType someOtherParameter) { }
            // ReSharper restore UnusedMember.Local
            // ReSharper restore UnusedParameter.Local
        }
    }
}
