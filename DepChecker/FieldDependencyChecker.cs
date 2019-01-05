using System.Reflection;

namespace DepChecker
{
    public class FieldDependencyChecker
    {
        private readonly TypeChecker _typeChecker;

        public FieldDependencyChecker(TypeChecker typeChecker)
        {
            _typeChecker = typeChecker;
        }

        public DependencyErrors Check(TypeInfo typeInHappyZone)
        {
            var errors = new DependencyErrors();

            foreach (var fieldInfo in typeInHappyZone.DeclaredFields)
            {
                var uglyTypeNames = _typeChecker.CheckType(fieldInfo.FieldType);

                foreach (var uglyTypeName in uglyTypeNames)
                {
                    errors.Append(new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, uglyTypeName));
                }
            }

            return errors;
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