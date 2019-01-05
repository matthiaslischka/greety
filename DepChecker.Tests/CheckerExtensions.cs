using System.Reflection;

namespace DepChecker.Tests
{
    public static class CheckerExtensions
    {
        public static DependencyErrors Check<T>(this ConstructorDependencyChecker checker)
        {
            return checker.Check(typeof(T).GetTypeInfo());
        }
    }
}