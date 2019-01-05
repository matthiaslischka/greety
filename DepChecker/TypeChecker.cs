using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DepChecker
{
    public interface ITypeChecker
    {
        IEnumerable<string> CheckType(Type typeToCheck);
    }

    public class TypeChecker : ITypeChecker
    {
        private readonly string[] _legalNamespaces;

        public TypeChecker(string legalNamespace)
        {
            _legalNamespaces = new[] {"System", legalNamespace};
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
                var illegalTypeNames = CheckType(type);
                foreach (var illegalTypeName in illegalTypeNames)
                {
                    yield return illegalTypeName;
                }
            }
        }

        private IEnumerable<string> CheckNonGenericType(Type typeToCheck)
        {
            var namespaceToCheck = typeToCheck.Namespace;
            Debug.Assert(namespaceToCheck != null);

            if (!_legalNamespaces.Any(ns => namespaceToCheck.StartsWith(ns)))
            {
                yield return typeToCheck.FullName;
            }
        }
    }
}