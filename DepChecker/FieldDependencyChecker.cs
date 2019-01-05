using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public class FieldDependencyChecker
    {
        private readonly ITypeChecker _typeChecker;

        public FieldDependencyChecker(ITypeChecker typeChecker)
        {
            _typeChecker = typeChecker;
        }

        public IEnumerable<FieldDependencyError> Check(TypeInfo typeInHappyZone)
        {
            foreach (var fieldInfo in typeInHappyZone.DeclaredFields)
            {
                var uglyTypeNames = _typeChecker.CheckType(fieldInfo.FieldType);

                foreach (var uglyTypeName in uglyTypeNames)
                {
                    yield return new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, uglyTypeName);
                }
            }
        }

        public class FieldDependencyError : DependencyErrorBase
        {
            public FieldDependencyError(string happyZoneTypeName, string fieldName, string nonHappyZoneTypeName)
                : base(happyZoneTypeName, fieldName, nonHappyZoneTypeName)
            {
            }

            public override string DependencyType { get; } = "field";
        }
    }
}