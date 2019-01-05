using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DepChecker
{
    public class FieldDependencyChecker : IDependencyChecker
    {
        private readonly INamespaceChecker _namespaceChecker;

        public FieldDependencyChecker(INamespaceChecker namespaceChecker)
        {
            _namespaceChecker = namespaceChecker;
        }

        public IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone)
        {
            return CheckLazy(typeInHappyZone).ToList();
        }

        private IEnumerable<FieldDependencyError> CheckLazy(TypeInfo typeInHappyZone)
        {
            foreach (var fieldInfo in typeInHappyZone.DeclaredFields)
            {
                var uglyTypeNames = _namespaceChecker.CheckType(fieldInfo.FieldType);

                foreach (var uglyTypeName in uglyTypeNames)
                {
                    yield return new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, uglyTypeName);
                }
            }
        }

        private class FieldDependencyError : DependencyErrorBase
        {
            public FieldDependencyError(string happyZoneTypeName, string fieldName, string nonHappyZoneTypeName)
                : base(happyZoneTypeName, fieldName, nonHappyZoneTypeName)
            {
            }

            public override string DependencyType { get; } = "field";
        }
    }
}