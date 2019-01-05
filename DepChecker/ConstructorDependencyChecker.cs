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

        public DependencyErrors Check(TypeInfo typeInHappyZone)
        {
            var errors = new DependencyErrors();

            foreach (var constructorInfo in typeInHappyZone.DeclaredConstructors)
            {
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    var dependingNamespace = parameterInfo.ParameterType.Namespace;
                    if (!dependingNamespace.StartsWith("System") &&
                        !dependingNamespace.StartsWith(_happyZoneNamespace))
                    {
                        errors.AddConstructorParameterError(typeInHappyZone, parameterInfo);
                    }
                }
            }

            return errors;
        }
    }
}