using System;
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
                    if (parameterInfo.ParameterType.IsGenericType)
                    {
                        CheckGenericType(parameterInfo.ParameterType, typeInHappyZone, parameterInfo, errors);
                    }
                    else
                    {
                        CheckNonGenericType(parameterInfo.ParameterType, typeInHappyZone, parameterInfo, errors);
                    }
                }
            }

            return errors;
        }

        private void CheckGenericType(Type typeToCheck, TypeInfo typeInHappyZone, ParameterInfo parameterInfo, DependencyErrors errors)
        {
            foreach (var type in typeToCheck.GenericTypeArguments)
            {
                CheckNonGenericType(type, typeInHappyZone, parameterInfo, errors);
            }
        }

        private void CheckNonGenericType(Type typeToCheck, TypeInfo typeInHappyZone, ParameterInfo parameterInfo, DependencyErrors errors)
        {
            var dependingNamespace = typeToCheck.Namespace;
            if (!dependingNamespace.StartsWith("System") &&
                !dependingNamespace.StartsWith(_happyZoneNamespace))
            {
                errors.Add(new ConstructorParameterDependencyError(typeInHappyZone.FullName, parameterInfo.Name, typeToCheck.FullName));
            }
        }

        public class ConstructorParameterDependencyError : DependencyErrorBase
        {
            public ConstructorParameterDependencyError(string happyZoneTypeName, string parameterName, string nonHappyZoneTypeName)
                : base(happyZoneTypeName, parameterName, nonHappyZoneTypeName)
            {
            }

            public override string DependencyType { get; } = "constructor parameter";
        }
    }
}