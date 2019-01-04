using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Greety.HappyZone;
using Xunit;
using Xunit.Abstractions;

namespace Greety.Tests.StaticCodeAnalysis
{
    public class DependencyChecker
    {
        private readonly ITestOutputHelper _testOutput;

        public DependencyChecker(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
        }

        [Theory]
        [ClassData(typeof(HappyZoneTypesProvider))]
        public void CheckType(TypeInfo typeInHappyZone)
        {
            var errors = new List<DependencyError>();

            CheckConstructorParameters(errors, typeInHappyZone);

            Dump(errors);

            errors.Should().BeEmpty();
        }

        private static void CheckConstructorParameters(ICollection<DependencyError> errors, TypeInfo typeInHappyZone)
        {
            foreach (var constructorInfo in typeInHappyZone.DeclaredConstructors)
            {
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    var dependingNamespace = parameterInfo.ParameterType.Namespace;
                    if (!dependingNamespace.StartsWith("System") &&
                        !dependingNamespace.StartsWith(HappyZoneTypesProvider.HappyZoneNamespace))
                    {
                        errors.Add(new DependencyError("Constructor", typeInHappyZone.FullName, parameterInfo.Name,
                            parameterInfo.ParameterType.FullName));
                    }
                }
            }
        }

        private void Dump(IEnumerable<DependencyError> errors)
        {
            foreach (var error in errors)
            {
                _testOutput.WriteLine($"{error.HappyZoneTypeName} references {error.NonHappyZoneTypeName} in parameter {error.ParameterName}.");
            }
        }
    }

    public class HappyZoneTypesProvider : TheoryData<TypeInfo>
    {
        private static readonly Type SomeTypeInHappyZone = typeof(Kernel);
        public static readonly string HappyZoneNamespace = SomeTypeInHappyZone.Namespace;

        public HappyZoneTypesProvider()
        {
            var assembly = SomeTypeInHappyZone.Assembly;

            foreach (var assemblyDefinedType in assembly.DefinedTypes
                .Where(t => t.Namespace != null)
                .Where(t => t.Namespace.StartsWith(HappyZoneNamespace)))
            {
                Add(assemblyDefinedType);
            }
        }
    }

    public class DependencyError
    {
        public string DependencyType { get; }
        public string HappyZoneTypeName { get; }
        public string ParameterName { get; }
        public string NonHappyZoneTypeName { get; }

        public DependencyError(string dependencyType, string happyZoneTypeName, string parameterName, string nonHappyZoneTypeName)
        {
            DependencyType = dependencyType;
            HappyZoneTypeName = happyZoneTypeName;
            ParameterName = parameterName;
            NonHappyZoneTypeName = nonHappyZoneTypeName;
        }
    }
}
