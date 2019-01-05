using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DepChecker
{
    public class DependencyChecker
    {
        public static DependencyChecker Create(string happyZoneNamespace)
        {
            var namespaceChecker = new NamespaceChecker();
            namespaceChecker.AddLegalNamespace(happyZoneNamespace);

            var checkers = new List<IDependencyChecker>
            {
                new ConstructorDependencyChecker(namespaceChecker),
                new FieldDependencyChecker(namespaceChecker),
            };

            return new DependencyChecker(checkers);
        }

        public IList<IDependencyChecker> Checkers { get; }

        private DependencyChecker(IEnumerable<IDependencyChecker> checkers)
        {
            Checkers = new List<IDependencyChecker>(checkers);
        }

        public IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone)
        {
            return Checkers.SelectMany(c => c.Check(typeInHappyZone))
                            .ToList();
        }
    }
}