using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DepChecker
{
    public class PropertyDependencyChecker : IDependencyChecker
    {
        private readonly INamespaceChecker _namespaceChecker;

        public PropertyDependencyChecker(INamespaceChecker namespaceChecker)
        {
            _namespaceChecker = namespaceChecker;
        }

        public IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone)
        {
            return CheckLazy(typeInHappyZone).ToList();
        }

        private IEnumerable<IDependencyError> CheckLazy(TypeInfo typeInHappyZone)
        {
            foreach (var propertyInfo in typeInHappyZone.DeclaredProperties)
            {
                var uglyTypeNames = _namespaceChecker.CheckType(propertyInfo.PropertyType);

                foreach (var uglyTypeName in uglyTypeNames)
                {
                    yield return null;
                    //yield return new PropertyDependencyError(typeInHappyZone.FullName, propertyInfo.Name, uglyTypeName);
                }
            }
        }

        private class PropertyDependencyError : DependencyErrorBase
        {
            public PropertyDependencyError(string happyZoneTypeName, string elementName, string nonHappyZoneTypeName) 
                : base(happyZoneTypeName, elementName, nonHappyZoneTypeName)
            {
            }

            public override string DependencyType { get; } = "property";
        }
    }
}