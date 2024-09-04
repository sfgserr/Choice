using BuildingBlocks.Application.Cqrs.Commands;
using Users.Domain.Categories;

namespace Users.Application.Users.Companies.Commands.FillData
{
    public class FillDataCommand : ICommand
    {
        public FillDataCommand(
            string description, 
            List<CategoryId> categoryIds, 
            List<string> photoUris, 
            bool isPrepaymentAvailable)
        {
            Description = description;
            CategoryIds = categoryIds;
            PhotoUris = photoUris;
            IsPrepaymentAvailable = isPrepaymentAvailable;
        }

        public string Description { get; }

        public List<CategoryId> CategoryIds { get; }

        public List<string> PhotoUris { get; }

        public bool IsPrepaymentAvailable { get; }
    }
}