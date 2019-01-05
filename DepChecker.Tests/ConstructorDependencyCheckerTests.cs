using Moq;
using Xunit;

namespace DepChecker.Tests
{
    public class ConstructorDependencyCheckerTests
    {
        private readonly ConstructorDependencyChecker _checker;
        private readonly Mock<ITypeChecker> _typeCheckerMock;

        public ConstructorDependencyCheckerTests()
        {
            _typeCheckerMock = new Mock<ITypeChecker>();
            _checker = new ConstructorDependencyChecker(_typeCheckerMock.Object);
        }

        [Fact]
        public void ShouldCheckAllParametersOfAllConstructors()
        {
            _checker.Check<Sample.ClassWithSeveralConstructors>();
            _typeCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeType)), Times.Exactly(2));
            _typeCheckerMock.Verify(tc => tc.CheckType(typeof(Sample.SomeOtherType)), Times.Once);
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