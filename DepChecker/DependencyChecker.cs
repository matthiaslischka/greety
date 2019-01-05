using System.Reflection;

namespace DepChecker
{
    public class DependencyChecker
    {
        private readonly ConstructorDependencyChecker _constructorDependencyChecker;
        private readonly FieldDependencyChecker _fieldDependencyChecker;

        public DependencyChecker(string happyZoneNamespace)
        {
            var typeChecker = new TypeChecker();
            typeChecker.AddLegalNamespace(happyZoneNamespace);

            _constructorDependencyChecker = new ConstructorDependencyChecker(typeChecker);
            _fieldDependencyChecker = new FieldDependencyChecker(typeChecker);
        }

        public DependencyErrors Check(TypeInfo typeInHappyZone)
        {
            var errors = new DependencyErrors();

            errors.Append(_constructorDependencyChecker.Check(typeInHappyZone));
            errors.Append(_fieldDependencyChecker.Check(typeInHappyZone));

            return errors;
        }
    }
}