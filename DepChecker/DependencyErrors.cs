using System.Collections;
using System.Collections.Generic;

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
    }
}