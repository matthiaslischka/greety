using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DepChecker
{
    public interface ITypeChecker
    {
        IEnumerable<string> CheckType(Type typeToCheck);
    }

    public class TypeChecker : ITypeChecker
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
                var uglyTypeNames = CheckType(type);
                foreach (var uglyTypeName in uglyTypeNames)
                {
                    yield return uglyTypeName;
                }
            }
        }

        private IEnumerable<string> CheckNonGenericType(Type typeToCheck)
        {
            var dependingNamespace = typeToCheck.Namespace;
            Debug.Assert(dependingNamespace != null);

            if (!dependingNamespace.StartsWith("System") &&
                !dependingNamespace.StartsWith(_happyZoneNamespace))
            {
                yield return typeToCheck.FullName;
            }
        }
    }
}