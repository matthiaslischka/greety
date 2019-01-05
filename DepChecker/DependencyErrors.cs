using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DepChecker
{
    public class DependencyErrors : IEnumerable<DependencyError>
    {
        private readonly List<DependencyError> _errors;

        public DependencyErrors()
        {
            _errors = new List<DependencyError>();
        }

        public IEnumerator<DependencyError> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _errors).GetEnumerator();
        }

        public void AddRange(IEnumerable<DependencyError> errors)
        {
            _errors.AddRange(errors);
        }

        public void AddConstructorParameterError(TypeInfo typeInHappyZone, ParameterInfo parameterInfo)
        {
            _errors.Add(new DependencyError("constructor parameter", typeInHappyZone.FullName, parameterInfo.Name, parameterInfo.ParameterType.FullName));
        }

        public void AddFieldDependencyError(TypeInfo typeInHappyZone, FieldInfo fieldInfo)
        {
            _errors.Add(new DependencyError("field", typeInHappyZone.FullName, fieldInfo.Name, fieldInfo.FieldType.FullName));
        }
    }
}