using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public static class DependencyErrorsExtensions
    {
        public static void AddConstructorParameterError(this List<DependencyError> errors, TypeInfo typeInHappyZone, ParameterInfo parameterInfo)
        {
            errors.Add(new DependencyError("constructor parameter", typeInHappyZone.FullName, parameterInfo.Name, parameterInfo.ParameterType.FullName));
        }

        public static void AddFieldDependencyError(this List<DependencyError> errors, TypeInfo typeInHappyZone,             FieldInfo fieldInfo)
        {
            errors.Add(new DependencyError("field", typeInHappyZone.FullName, fieldInfo.Name, fieldInfo.FieldType.FullName));
        }
    }
}