using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public class ConstructorDependencyChecker
    {
        private readonly TypeChecker _typeChecker;

        public ConstructorDependencyChecker(TypeChecker typeChecker)
        {
            _typeChecker = typeChecker;
        }

        public IEnumerable<ConstructorParameterDependencyError> Check(TypeInfo typeInHappyZone)
        {
            foreach (var constructorInfo in typeInHappyZone.DeclaredConstructors)
            {
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    var uglyTypeNames = _typeChecker.CheckType(parameterInfo.ParameterType);

                    foreach (var uglyTypeName in uglyTypeNames)
                    {
                        yield return new ConstructorParameterDependencyError(typeInHappyZone.FullName, parameterInfo.Name, uglyTypeName);
                    }
                }
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