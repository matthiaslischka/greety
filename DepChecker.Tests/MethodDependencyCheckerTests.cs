using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using Moq;
using Xunit;

namespace DepChecker.Tests
{
    public class MethodDependencyCheckerTests
    {
        private readonly MethodDependencyChecker _checker;
        private readonly Mock<INamespaceChecker> _namespaceCheckerMock;

        public MethodDependencyCheckerTests()
        {
            _namespaceCheckerMock = new Mock<INamespaceChecker>();
            _checker = new MethodDependencyChecker(_namespaceCheckerMock.Object);
        }

        [Fact]
        public void ShouldCheckAllParametersInAllMethods()
        {
            _checker.Check(typeof(ClassWithSeveralMethods).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SampleTypes.SomeType)), Times.Exactly(2));
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SampleTypes.SomeOtherType)), Times.Once);
        }

        [Fact]
        public void ShouldCheckReturnType()
        {
            _checker.Check(typeof(ClassWithSeveralMethods).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SampleTypes.AnotherType)), Times.Once);
        }

        [Fact]
        public void ShouldReturnFoundParameterTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(SampleTypes.Ugly.UglyType))).Returns(new[] { "UglyType" });

            var errors = _checker.Check(typeof(ClassWithSeveralMethods).GetTypeInfo());

            errors.Should().Contain(ParameterDependencyError("uglyParameter", "UglyType"));
        }

        [Fact]
        public void ShouldReturnFoundReturnTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(SampleTypes.Ugly.UglyType))).Returns(new[] { "UglyType" });

            var errors = _checker.Check(typeof(ClassWithSeveralMethods).GetTypeInfo());

            errors.Should().Contain(ResultDependencyError("UglyType"));
        }

        private Expression<Func<IDependencyError, bool>> ParameterDependencyError(string uglyParameterName, string uglyTypeName)
        {
            return err => err is MethodDependencyChecker.MethodParameterDependencyError &&
                          err.ElementName == uglyParameterName &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }

        private Expression<Func<IDependencyError, bool>> ResultDependencyError(string uglyTypeName)
        {
            return err => err is MethodDependencyChecker.MethodResultDependencyError &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }

        class ClassWithSeveralMethods
        {
            // ReSharper disable UnusedMember.Local
            // ReSharper disable UnusedParameter.Local
            private void Method(SampleTypes.SomeType param) { }
            private void OtherMethod(SampleTypes.SomeOtherType param1, SampleTypes.SomeType param2) { }
            private SampleTypes.AnotherType MethodWithReturnValue() => null;

            private void MethodWithUglyParameter(SampleTypes.Ugly.UglyType uglyParameter) { }
            private SampleTypes.Ugly.UglyType MethodWithUglyReturnType() => null;
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
        }
    }
}