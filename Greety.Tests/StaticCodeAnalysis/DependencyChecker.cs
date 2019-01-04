using System.Collections.Generic;
using System.Reflection;

namespace Greety.Tests.StaticCodeAnalysis
{
    public class DependencyChecker
    {
        private readonly ConstructorDependencyChecker _constructorDependencyChecker;
        private List<DependencyError> _errors;

        public DependencyChecker(string happyZoneNamespace)
        {
            _errors = new List<DependencyError>();
            _constructorDependencyChecker = new ConstructorDependencyChecker(_errors, happyZoneNamespace);
        }

        public IEnumerable<DependencyError> Errors => _errors;

        public void Check(TypeInfo typeInHappyZone)
        {
            _constructorDependencyChecker.Check(typeInHappyZone);
        }
    }
}