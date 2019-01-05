using System.Reflection;
using Moq;
using Sample;
using Sample.Nice;
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

            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SomeType)), Times.Once);
            _namespaceCheckerMock.Verify(tc => tc.CheckType(typeof(SomeOtherType)), Times.Once);
        }
    }
}

namespace Sample
{
    namespace Nice
    {
        // ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable 169
        class ClassWithSeveralFields
        {
            private SomeType _field;
            private SomeOtherType _otherField;
        }
#pragma warning restore 169
        // ReSharper enable ClassNeverInstantiated.Global
    }
}