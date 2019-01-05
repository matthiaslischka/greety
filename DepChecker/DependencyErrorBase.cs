namespace DepChecker
{
    public interface IDependencyError
    {
        string DependencyType { get; }
        string HappyZoneTypeName { get; }
        string ElementName { get; }
        string NonHappyZoneTypeName { get; }
    }

    public abstract class DependencyErrorBase : IDependencyError
    {
        public abstract string DependencyType { get; }
        public string HappyZoneTypeName { get; }
        public string ElementName { get; }
        public string NonHappyZoneTypeName { get; }

        protected DependencyErrorBase(string happyZoneTypeName, string elementName, string nonHappyZoneTypeName)
        {
            HappyZoneTypeName = happyZoneTypeName;
            ElementName = elementName;
            NonHappyZoneTypeName = nonHappyZoneTypeName;
        }
    }
}