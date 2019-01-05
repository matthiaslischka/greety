using System.Reflection;

namespace DepChecker
{
    public class FieldDependencyChecker
    {
        private readonly string _happyZoneNamespace;

        public FieldDependencyChecker(string happyZoneNamespace)
        {
            _happyZoneNamespace = happyZoneNamespace;
        }

        public DependencyErrors Check(TypeInfo typeInHappyZone)
        {
            var errors = new DependencyErrors();

            foreach (var fieldInfo in typeInHappyZone.DeclaredFields)
            {
                var dependingNamespace = fieldInfo.FieldType.Namespace;
                if (!dependingNamespace.StartsWith("System") &&
                    !dependingNamespace.StartsWith(_happyZoneNamespace))
                {
                    errors.Add(new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, fieldInfo.FieldType.FullName));
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