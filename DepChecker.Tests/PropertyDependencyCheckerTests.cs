using System.Reflection;
using Moq;
using Sample;
using Sample.Nice;
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

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SomeType)), Times.Once);
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SomeOtherType)), Times.Once);
        }
    }
}

namespace Sample
{
    namespace Nice
    {
        class ClassWithSeveralProperties
        {
            private SomeType Field { get; }
            private SomeOtherType OtherField { get; }
        }
    }
}