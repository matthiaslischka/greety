using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public class DependencyChecker
    {
        private readonly ConstructorDependencyChecker _constructorDependencyChecker;
        private readonly FieldDependencyChecker _fieldDependencyChecker;

        public DependencyChecker(string happyZoneNamespace)
        {
            var namespaceChecker = new NamespaceChecker();
            namespaceChecker.AddLegalNamespace(happyZoneNamespace);

            _constructorDependencyChecker = new ConstructorDependencyChecker(namespaceChecker);
            _fieldDependencyChecker = new FieldDependencyChecker(namespaceChecker);
        }

        public IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone)
        {
            var errors = new List<IDependencyError>();

            errors.AddRange(_constructorDependencyChecker.Check(typeInHappyZone));
            errors.AddRange(_fieldDependencyChecker.Check(typeInHappyZone));

            return errors.AsReadOnly();
        }
    }
}