using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Greety.Tests.StaticCodeAnalysis
{
    public class DependencyChecks
    {
        private readonly ITestOutputHelper _testOutput;

        public DependencyChecks(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
        }

        [Theory]
        [ClassData(typeof(HappyZoneTypesProvider))]
        public void CheckType(TypeInfo typeInHappyZone)
        {
            ConstructorDependencyChecker _constructorDependencyChecker;

            var errors = new List<DependencyError>();

            _constructorDependencyChecker = new ConstructorDependencyChecker(errors, HappyZoneTypesProvider.HappyZoneNamespace);
            _constructorDependencyChecker.Check(typeInHappyZone);

            Dump(errors);

            errors.Should().BeEmpty();
        }

        private void Dump(IEnumerable<DependencyError> errors)
        {
            foreach (var error in errors)
            {
                _testOutput.WriteLine($"{error.HappyZoneTypeName} references {error.NonHappyZoneTypeName} in parameter {error.ParameterName}.");
            }
        }
    }
}
