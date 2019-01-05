using System.Reflection;
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
            _checker.Check(typeof(Sample.ClassWithSeveralMethods).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeType)), Times.Exactly(2));
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeOtherType)), Times.Once);
        }

        [Fact]
        public void ShouldCheckReturnType()
        {
            _checker.Check(typeof(Sample.ClassWithSeveralMethods).GetTypeInfo());

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.AnotherType)), Times.Once);
        }
    }
}

namespace Sample
{
    class ClassWithSeveralMethods
    {
        // ReSharper disable UnusedMember.Local
        // ReSharper disable UnusedParameter.Local
        private void Method(SomeType param) { }
        private void OtherMethod(SomeOtherType param1, SomeType param2) { }
        private AnotherType MethodWithReturnValue() => null;
        // ReSharper restore UnusedParameter.Local
        // ReSharper restore UnusedMember.Local
    }
}