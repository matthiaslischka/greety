namespace DepChecker
{
    public interface IDependencyError
    {
        string DependencyType { get; }
        string HappyZoneTypeName { get; }
        string ElementName { get; }
        string NonHappyZoneTypeName { get; }
    }

    abstract class DependencyError : IDependencyError
    {
        public abstract string DependencyType { get; }
        public string HappyZoneTypeName { get; }
        public string ElementName { get; }
        public string NonHappyZoneTypeName { get; }

        protected DependencyError(string happyZoneTypeName, string elementName, string nonHappyZoneTypeName)
        {
            HappyZoneTypeName = happyZoneTypeName;
            ElementName = elementName;
            NonHappyZoneTypeName = nonHappyZoneTypeName;
        }
    }

    class FieldDependencyError : DependencyError
    {
        public FieldDependencyError(string happyZoneTypeName, string elementName, string nonHappyZoneTypeName) 
            : base(happyZoneTypeName, elementName, nonHappyZoneTypeName)
        {
        }

        public override string DependencyType { get; } = "field";
    }

    class ConstructorParameterDependencyError : DependencyError
    {
        public ConstructorParameterDependencyError(string happyZoneTypeName, string elementName, string nonHappyZoneTypeName) 
            : base(happyZoneTypeName, elementName, nonHappyZoneTypeName)
        {
        }

        public override string DependencyType { get; } = "constructor parameter";
    }
}