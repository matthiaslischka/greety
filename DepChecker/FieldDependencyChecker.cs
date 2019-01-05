using System.Reflection;

namespace DepChecker
{
    internal class FieldDependencyChecker
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
                    errors.AddFieldDependencyError(typeInHappyZone, fieldInfo);
                }
            }

            return errors;
        }
    }
}