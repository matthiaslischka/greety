using System.Collections.Generic;
using System.Reflection;

namespace Greety.Tests.StaticCodeAnalysis
{
    public class DependencyChecker
    {
        private readonly ConstructorDependencyChecker _constructorDependencyChecker;

        public DependencyChecker(string happyZoneNamespace)
        {
            _constructorDependencyChecker = new ConstructorDependencyChecker(happyZoneNamespace);
        }

        public List<DependencyError> Check(TypeInfo typeInHappyZone)
        {
            var errors = new List<DependencyError>();

            errors.AddRange(_constructorDependencyChecker.Check(typeInHappyZone));

            return errors;
        }
    }
}