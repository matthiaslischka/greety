using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DepChecker
{
    public class PropertyDependencyChecker : IDependencyChecker
    {
        private readonly NamespaceChecker _namespaceChecker;

        public PropertyDependencyChecker(NamespaceChecker namespaceChecker)
        {
            _namespaceChecker = namespaceChecker;
        }

        public IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone)
        {
            return CheckLazy(typeInHappyZone).ToList();
        }

        private IEnumerable<IDependencyError> CheckLazy(TypeInfo typeInHappyZone)
        {
            yield break;
        }
    }
}