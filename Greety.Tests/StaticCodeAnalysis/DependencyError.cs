namespace Greety.Tests.StaticCodeAnalysis
{
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