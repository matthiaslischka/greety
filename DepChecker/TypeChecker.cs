using System;
using System.Collections.Generic;

namespace DepChecker
{
    public class TypeChecker
    {
        private readonly string _happyZoneNamespace;

        public TypeChecker(string happyZoneNamespace)
        {
            _happyZoneNamespace = happyZoneNamespace;
        }

        public IEnumerable<string> CheckType(Type typeToCheck)
        {
            return typeToCheck.IsGenericType
                ? CheckGenericType(typeToCheck)
                : CheckNonGenericType(typeToCheck);
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
    }
}