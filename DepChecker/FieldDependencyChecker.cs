using System;
using System.Collections.Generic;
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
                var typeToCheck = fieldInfo.FieldType;

                var uglyTypeNames = typeToCheck.IsGenericType
                    ? CheckGenericType(typeToCheck)
                    : CheckNonGenericType(typeToCheck);

                foreach (var uglyTypeName in uglyTypeNames)
                {
                    errors.Append(new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, uglyTypeName));
                }
            }

            return errors;
        }

        private IEnumerable<string> CheckGenericType(Type typeToCheck)
        {
            foreach (var type in typeToCheck.GenericTypeArguments)
            {
                var uglyTypeNames = CheckNonGenericType(type);
                foreach (var uglyTypeName in uglyTypeNames)
                {
                    yield return uglyTypeName;
                }
            }
        }

        private IEnumerable<string> CheckNonGenericType(Type typeToCheck)
        {
            var dependingNamespace = typeToCheck.Namespace;
            if (!dependingNamespace.StartsWith("System") &&
                !dependingNamespace.StartsWith(_happyZoneNamespace))
            {
                yield return typeToCheck.FullName;
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