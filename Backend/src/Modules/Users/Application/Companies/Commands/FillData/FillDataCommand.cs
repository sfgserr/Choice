using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Categories;

namespace Users.Application.Companies.Commands.FillData
{
    public class FillDataCommand : ICommand
    {
        public FillDataCommand(
            string description, 
            List<CategoryId> categoryIds, 
            List<string> photoUris,
            List<string> socialMediaUris,
            bool isPrepaymentAvailable)
        {
            Description = description;
            CategoryIds = categoryIds;
            PhotoUris = photoUris;
            IsPrepaymentAvailable = isPrepaymentAvailable;
            SocialMediaUris = socialMediaUris;
        }

        public string Description { get; }

        public List<CategoryId> CategoryIds { get; }

        public List<string> PhotoUris { get; }
        
        public List<string> SocialMediaUris { get; }
        
        public bool IsPrepaymentAvailable { get; }
    }
}