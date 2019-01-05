using System.Collections;
using System.Collections.Generic;

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

        public void Add(IDependencyError error)
        {
            _errors.Add(error);
        }

        public void Append(DependencyErrors errors)
        {
            _errors.AddRange(errors);
        }
    }
}