using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public class ConstructorDependencyChecker
    {
        private readonly string _happyZoneNamespace;

        public ConstructorDependencyChecker(string happyZoneNamespace)
        {
            _happyZoneNamespace = happyZoneNamespace;
        }

        public IEnumerable<DependencyError> Check(TypeInfo typeInHappyZone)
        {
            var errors = new List<DependencyError>();

            foreach (var constructorInfo in typeInHappyZone.DeclaredConstructors)
            {
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    var dependingNamespace = parameterInfo.ParameterType.Namespace;
                    if (!dependingNamespace.StartsWith("System") &&
                        !dependingNamespace.StartsWith(_happyZoneNamespace))
                    {
                        errors.Add(new DependencyError("constructor parameter", typeInHappyZone.FullName, parameterInfo.Name,
                            parameterInfo.ParameterType.FullName));
                    }
                }
            }

            return errors;
        }
    }
}