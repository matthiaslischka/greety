using System;
using System.Linq;
using System.Reflection;
using Greety.HappyZone;
using Xunit;

namespace Greety.Tests.StaticCodeAnalysis
{
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
}