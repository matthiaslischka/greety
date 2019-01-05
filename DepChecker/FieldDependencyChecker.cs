using System.Collections.Generic;
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

        public IEnumerable<DependencyError> Check(TypeInfo typeInHappyZone)
        {
            var errors = new List<DependencyError>();

            foreach (var fieldInfo in typeInHappyZone.DeclaredFields)
            {
                var dependingNamespace = fieldInfo.FieldType.Namespace;
                if (!dependingNamespace.StartsWith("System") &&
                    !dependingNamespace.StartsWith(_happyZoneNamespace))
                {
                    errors.Add(new DependencyError("field", typeInHappyZone.FullName, fieldInfo.Name,
                        fieldInfo.FieldType.FullName));
                }
            }

            return errors;
        }
    }
}