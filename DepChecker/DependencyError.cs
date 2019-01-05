namespace DepChecker
{
    public class DependencyError
    {
        public string DependencyType { get; }
        public string HappyZoneTypeName { get; }
        public string ElementName { get; }
        public string NonHappyZoneTypeName { get; }

        public DependencyError(string dependencyType, string happyZoneTypeName, string elementName, string nonHappyZoneTypeName)
        {
            DependencyType = dependencyType;
            HappyZoneTypeName = happyZoneTypeName;
            ElementName = elementName;
            NonHappyZoneTypeName = nonHappyZoneTypeName;
        }
    }
}