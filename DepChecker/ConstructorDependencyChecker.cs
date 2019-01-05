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
                        errors.Add(new ConstructorParameterDependencyError(typeInHappyZone.FullName, parameterInfo.Name, parameterInfo.ParameterType.FullName));
                    }
                }
            }

            return errors;
        }

        private class ConstructorParameterDependencyError : DependencyErrorBase
        {
            public ConstructorParameterDependencyError(string happyZoneTypeName, string elementName, string nonHappyZoneTypeName)
                : base(happyZoneTypeName, elementName, nonHappyZoneTypeName)
            {
            }

            public override string DependencyType { get; } = "constructor parameter";
        }
    }
}