using System.Collections.Generic;
using System.Reflection;

namespace Greety.Tests.StaticCodeAnalysis
{
    public class ConstructorDependencyChecker
    {
        private readonly List<DependencyError> _errors;
        private readonly string _happyZoneNamespace;

        public ConstructorDependencyChecker(List<DependencyError> errors, string happyZoneNamespace)
        {
            _errors = errors;
            _happyZoneNamespace = happyZoneNamespace;
        }

        public void Check(TypeInfo typeInHappyZone)
        {
            foreach (var constructorInfo in typeInHappyZone.DeclaredConstructors)
            {
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    var dependingNamespace = parameterInfo.ParameterType.Namespace;
                    if (!dependingNamespace.StartsWith("System") &&
                        !dependingNamespace.StartsWith(_happyZoneNamespace))
                    {
                        _errors.Add(new DependencyError("Constructor", typeInHappyZone.FullName, parameterInfo.Name,
                            parameterInfo.ParameterType.FullName));
                    }
                }
            }
        }
    }
}