using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DepChecker
{
    public class DependencyChecker
    {
        private readonly IList<IDependencyChecker> _checkers;

        public static DependencyChecker Create(string happyZoneNamespace)
        {
            var namespaceChecker = new NamespaceChecker();
            namespaceChecker.AddLegalNamespace(happyZoneNamespace);

            var checkers = new List<IDependencyChecker>
            {
                new ConstructorDependencyChecker(namespaceChecker),
                new FieldDependencyChecker(namespaceChecker)
            };

            return new DependencyChecker(checkers);
        }

        private DependencyChecker(IEnumerable<IDependencyChecker> checkers)
        {
            _checkers = new List<IDependencyChecker>(checkers);
        }

        public IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone)
        {
            return _checkers.SelectMany(c => c.Check(typeInHappyZone))
                            .ToList();
        }
    }
}