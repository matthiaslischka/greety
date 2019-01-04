using System.Collections.Generic;
using System.Reflection;

namespace Greety.Tests.StaticCodeAnalysis
{
    public class DependencyChecker
    {
        private readonly ConstructorDependencyChecker _constructorDependencyChecker;
        private readonly FieldDependencyChecker _fieldDependencyChecker;

        public DependencyChecker(string happyZoneNamespace)
        {
            _constructorDependencyChecker = new ConstructorDependencyChecker(happyZoneNamespace);
            _fieldDependencyChecker = new FieldDependencyChecker(happyZoneNamespace);
        }

        public List<DependencyError> Check(TypeInfo typeInHappyZone)
        {
            var errors = new List<DependencyError>();

            errors.AddRange(_constructorDependencyChecker.Check(typeInHappyZone));
            errors.AddRange(_fieldDependencyChecker.Check(typeInHappyZone));

            return errors;
        }
    }
}