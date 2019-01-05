using System.Collections.Generic;
using System.Reflection;

namespace DepChecker.Tests
{
    public static class CheckerExtensions
    {
        public static IEnumerable<DependencyError> Check<T>(this ConstructorDependencyChecker checker)
        {
            return checker.Check(typeof(T).GetTypeInfo());
        }
    }
}