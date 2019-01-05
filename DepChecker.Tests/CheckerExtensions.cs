using System.Reflection;

namespace DepChecker.Tests
{
    public static class CheckerExtensions
    {
        public static void Check<T>(this ConstructorDependencyChecker checker)
        {
            checker.Check(typeof(T).GetTypeInfo());
        }

        public static void Check<T>(this FieldDependencyChecker checker)
        {
            checker.Check(typeof(T).GetTypeInfo());
        }
    }
}