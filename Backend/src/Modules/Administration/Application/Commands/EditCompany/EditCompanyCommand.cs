using BuildingBlocks.Application.Cqrs.Commands;

namespace Administration.Application.Commands.EditCompany
{
    public class EditCompanyCommand : ICommand
    {
        public EditCompanyCommand(
            Guid id, 
            string iconUri,
            string name, 
            string description, 
            string email, 
            string phoneNumber, 
            string city, 
            string street, 
            List<string> socialMedias, 
            List<int> categoryIds, 
            List<string> photoUris, 
            bool isPrepaymentAvailable)
        {
            Id = id;
            IconUri = iconUri;
            Name = name;
            Description = description;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            SocialMedias = socialMedias;
            CategoryIds = categoryIds;
            PhotoUris = photoUris;
            IsPrepaymentAvailable = isPrepaymentAvailable;
        }

        public Guid Id { get; }

        public string IconUri { get; }
        
        public string Name { get; }
        
        public string Description { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string City { get; }
        
        public string Street { get; }

        public List<string> SocialMedias { get; }

        public List<int> CategoryIds { get; }

        public List<string> PhotoUris { get; }

        public bool IsPrepaymentAvailable { get; }
    }
}