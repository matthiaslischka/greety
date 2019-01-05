using System;
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
                if (fieldInfo.FieldType.IsGenericType)
                {
                    CheckGenericType(fieldInfo.FieldType, typeInHappyZone, fieldInfo, errors);
                }
                else
                {
                    CheckNonGenericType(fieldInfo.FieldType, typeInHappyZone, fieldInfo, errors);
                }
            }

            return errors;
        }

        private void CheckGenericType(Type typeToCheck, TypeInfo typeInHappyZone, FieldInfo fieldInfo, DependencyErrors errors)
        {
            foreach (var type in typeToCheck.GenericTypeArguments)
            {
                CheckNonGenericType(type, typeInHappyZone, fieldInfo, errors);
            }
        }

        private void CheckNonGenericType(Type typeToCheck, TypeInfo typeInHappyZone, FieldInfo fieldInfo, DependencyErrors errors)
        {
            var dependingNamespace = typeToCheck.Namespace;
            if (!dependingNamespace.StartsWith("System") &&
                !dependingNamespace.StartsWith(_happyZoneNamespace))
            {
                errors.Add(new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, typeToCheck.FullName));
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