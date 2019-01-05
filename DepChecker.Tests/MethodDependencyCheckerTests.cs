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

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeType)), Times.Exactly(2));
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeOtherType)), Times.Once);
        }

        [Fact]
        public void ShouldCheckReturnType()
        {
            _checker.Check(typeof(ClassWithSeveralMethods).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.AnotherType)), Times.Once);
        }

        [Fact]
        public void ShouldReturnFoundParameterTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(Sample.Ugly.UglyType))).Returns(new[] { "UglyType" });

            var errors = _checker.Check(typeof(ClassWithSeveralMethods).GetTypeInfo());

            errors.Should().Contain(ParameterDependencyError("uglyParameter", "UglyType"));
        }

        [Fact]
        public void ShouldReturnFoundReturnTypesAsDependencyErrors()
        {
            _namespaceCheckerMock.Setup(nc => nc.CheckType(typeof(Sample.Ugly.UglyType))).Returns(new[] { "UglyType" });

            var errors = _checker.Check(typeof(ClassWithSeveralMethods).GetTypeInfo());

            errors.Should().Contain(ResultDependencyError("UglyType"));
        }

        private Expression<Func<IDependencyError, bool>> ParameterDependencyError(string uglyParameterName, string uglyTypeName)
        {
            return err => err is MethodDependencyChecker.ParameterDependencyError &&
                          err.ElementName == uglyParameterName &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }

        private Expression<Func<IDependencyError, bool>> ResultDependencyError(string uglyTypeName)
        {
            return err => err is MethodDependencyChecker.ResultDependencyError &&
                          err.NonHappyZoneTypeName.EndsWith(uglyTypeName);
        }

        class ClassWithSeveralMethods
        {
            // ReSharper disable UnusedMember.Local
            // ReSharper disable UnusedParameter.Local
            private void Method(Sample.SomeType param) { }
            private void OtherMethod(Sample.SomeOtherType param1, Sample.SomeType param2) { }
            private Sample.AnotherType MethodWithReturnValue() => null;

            private void MethodWithUglyParameter(Sample.Ugly.UglyType uglyParameter) { }
            private Sample.Ugly.UglyType MethodWithUglyReturnType() => null;
            // ReSharper restore UnusedParameter.Local
            // ReSharper restore UnusedMember.Local
        }
    }
}