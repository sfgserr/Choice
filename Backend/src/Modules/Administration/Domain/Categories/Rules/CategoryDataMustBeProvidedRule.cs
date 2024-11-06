using BuildingBlocks.Domain;

namespace Administration.Domain.Categories.Rules
{
    internal class CategoryDataMustBeProvidedRule : IBusinessRule
    {
        private readonly string _title;
        private readonly string _iconUri;

        internal CategoryDataMustBeProvidedRule(string title, string iconUri)
        {
            _title = title;
            _iconUri = iconUri;
        }

        public bool IsBroken => string.IsNullOrEmpty(_title) || string.IsNullOrEmpty(_iconUri);

        public string Message => "Category data must be provided";
    }
}