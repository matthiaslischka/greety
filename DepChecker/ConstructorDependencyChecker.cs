using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DepChecker
{
    public class ConstructorDependencyChecker
    {
        private readonly ITypeChecker _typeChecker;

        public ConstructorDependencyChecker(ITypeChecker typeChecker)
        {
            _typeChecker = typeChecker;
        }

        public IReadOnlyCollection<ConstructorParameterDependencyError> Check(TypeInfo typeInHappyZone)
        {
            return CheckLazy(typeInHappyZone).ToList();
        }

        private IEnumerable<ConstructorParameterDependencyError> CheckLazy(TypeInfo typeInHappyZone)
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