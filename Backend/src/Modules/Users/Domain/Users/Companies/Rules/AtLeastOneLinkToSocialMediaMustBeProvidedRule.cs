using BuildingBlocks.Domain;

namespace Users.Domain.Users.Companies.Rules
{
    internal class AtLeastOneLinkToSocialMediaMustBeProvidedRule : IBusinessRule
    {
        private readonly List<SocialMedia> _socialMediaUris;

        internal AtLeastOneLinkToSocialMediaMustBeProvidedRule(List<SocialMedia> socialMediaUris)
        {
            _socialMediaUris = socialMediaUris;
        }

        public bool IsBroken => _socialMediaUris.Count == 0;

        public string Message { get; } = "Укажите хотя бы одну социальную сеть";
    }
}