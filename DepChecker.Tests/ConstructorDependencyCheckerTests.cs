using System.Reflection;
using Moq;
using Sample;
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

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeType)), Times.Exactly(2));
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeOtherType)), Times.Once);
        }
    }
}

namespace Sample
{
    // ReSharper disable UnusedParameter.Local
    // ReSharper disable UnusedMember.Global
    class ClassWithSeveralConstructors
    {
        public ClassWithSeveralConstructors(SomeType someParameter) { }
        public ClassWithSeveralConstructors(SomeType someParameter, SomeOtherType someOtherParameter) { }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore UnusedParameter.Local
}