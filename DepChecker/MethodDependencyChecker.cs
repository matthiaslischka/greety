using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DepChecker
{
    public class MethodDependencyChecker : IDependencyChecker
    {
        private readonly INamespaceChecker _namespaceChecker;

        public MethodDependencyChecker(INamespaceChecker namespaceChecker)
        {
            _namespaceChecker = namespaceChecker;
        }

        public IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone)
        {
            return CheckLazy(typeInHappyZone).ToList();
        }

        private IEnumerable<IDependencyError> CheckLazy(TypeInfo typeInHappyZone)
        {
            foreach (var methodInfo in typeInHappyZone.DeclaredMethods)
            {
                foreach (var dependencyError1 in CheckReturnType(typeInHappyZone, methodInfo))
                {
                    yield return dependencyError1;
                }

                foreach (var dependencyError in CheckParameters(typeInHappyZone, methodInfo))
                {
                    yield return dependencyError;
                }
            }
        }

        private IEnumerable<IDependencyError> CheckReturnType(TypeInfo typeInHappyZone, MethodInfo methodInfo)
        {
            var invalidTypeNames = _namespaceChecker.CheckType(methodInfo.ReturnType);
            foreach (var invalidTypeName in invalidTypeNames)
            {
                yield return null;
                //yield return new MethodDependencyError(typeInHappyZone.FullName, "(return)", invalidTypeName);
            }
        }

        private IEnumerable<IDependencyError> CheckParameters(TypeInfo typeInHappyZone, MethodInfo methodInfo)
        {
            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                var invalidTypeNames = _namespaceChecker.CheckType(parameterInfo.ParameterType);

                foreach (var invalidTypeName in invalidTypeNames)
                {
                    yield return null;
                    //yield return new MethodDependencyError(typeInHappyZone.FullName, parameterInfo.Name, invalidTypeName);
                }
            }
        }

        private class MethodDependencyError : DependencyErrorBase
        {
            public MethodDependencyError(string happyZoneTypeName, string elementName, string nonHappyZoneTypeName) :
                base(happyZoneTypeName, elementName, nonHappyZoneTypeName)
            {
            }

            public override string DependencyType { get; } = "method";
        }
    }
}