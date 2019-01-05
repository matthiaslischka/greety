using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public class DependencyErrors : IEnumerable<IDependencyError>
    {
        private readonly List<IDependencyError> _errors;

        public DependencyErrors()
        {
            _errors = new List<IDependencyError>();
        }

        public IEnumerator<IDependencyError> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _errors).GetEnumerator();
        }

        public void AddConstructorParameterError(TypeInfo typeInHappyZone, ParameterInfo parameterInfo)
        {
            _errors.Add(new ConstructorParameterDependencyError(typeInHappyZone.FullName, parameterInfo.Name, parameterInfo.ParameterType.FullName));
        }

        public void AddFieldDependencyError(TypeInfo typeInHappyZone, FieldInfo fieldInfo)
        {
            _errors.Add(new FieldDependencyError(typeInHappyZone.FullName, fieldInfo.Name, fieldInfo.FieldType.FullName));
        }

        public void Append(DependencyErrors errors)
        {
            _errors.AddRange(errors);
        }
    }
}