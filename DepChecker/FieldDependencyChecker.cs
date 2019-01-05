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
                IEnumerable<IDependencyError> errs;
                if (fieldInfo.FieldType.IsGenericType)
                {
                    errs = CheckGenericType(fieldInfo.FieldType, typeInHappyZone, fieldInfo);
                }
                else
                {
                    errs = CheckNonGenericType(fieldInfo.FieldType, typeInHappyZone, fieldInfo);
                }
                errors.Append(errs);
            }

            return errors;
        }

        private IEnumerable<IDependencyError> CheckGenericType(Type typeToCheck, TypeInfo typeInHappyZone, FieldInfo fieldInfo)
        {
            foreach (var type in typeToCheck.GenericTypeArguments)
            {
                var errs = CheckNonGenericType(type, typeInHappyZone, fieldInfo);
                foreach (var dependencyError in errs)
                {
                    yield return dependencyError;
                }
            }
        }

        private IEnumerable<IDependencyError> CheckNonGenericType(Type typeToCheck, TypeInfo typeInHappyZone, FieldInfo fieldInfo)
        {
            var dependingNamespace = typeToCheck.Namespace;
            if (!dependingNamespace.StartsWith("System") &&
                !dependingNamespace.StartsWith(_happyZoneNamespace))
            {
                yield return new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, typeToCheck.FullName);
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