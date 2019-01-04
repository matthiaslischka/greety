using System.Collections.Generic;
using System.Reflection;

namespace Greety.Tests.StaticCodeAnalysis
{
    public class ConstructorDependencyChecker
    {
        private readonly List<DependencyError> _errors;

        public ConstructorDependencyChecker(List<DependencyError> errors)
        {
            _errors = errors;
        }

        public void Check(TypeInfo typeInHappyZone)
        {
            foreach (var constructorInfo in typeInHappyZone.DeclaredConstructors)
            {
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    var dependingNamespace = parameterInfo.ParameterType.Namespace;
                    if (!dependingNamespace.StartsWith("System") &&
                        !dependingNamespace.StartsWith(HappyZoneTypesProvider.HappyZoneNamespace))
                    {
                        _errors.Add(new DependencyError("Constructor", typeInHappyZone.FullName, parameterInfo.Name,
                            parameterInfo.ParameterType.FullName));
                    }
                }
            }
        }
    }
}