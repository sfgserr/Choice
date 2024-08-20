using BuildingBlocks.Domain;

namespace Users.Domain.Users.Rules
{
    internal class FieldsMustBeProvidedRule : IBusinessRule
    {
        private readonly string[] _fields;

        internal FieldsMustBeProvidedRule(string[] fields)
        {
            _fields = fields;
        }

        public bool IsBroken => _fields.Any(string.IsNullOrEmpty);

        public string Message => "All fields must be provided";
    }
}
