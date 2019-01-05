using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public interface IDependencyChecker
    {
        IReadOnlyCollection<IDependencyError> Check(TypeInfo typeInHappyZone);
    }
}