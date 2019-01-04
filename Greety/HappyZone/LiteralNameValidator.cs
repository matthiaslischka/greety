using System.Linq;

namespace Greety.HappyZone
{
    public class LiteralNameValidator : INameValidator
    {
        private readonly string[] _validNames;

        public LiteralNameValidator(params string[] validNames)
        {
            _validNames = validNames;
        }

        public bool IsValid(string name)
        {
            return _validNames.Contains(name);
        }
    }
}