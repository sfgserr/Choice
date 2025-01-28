using Users.Domain.Categories;
using Users.Domain.Users.Companies;

namespace Users.Application.Companies.Queries.GetCompany
{
    public class CompanyDto
    {
        public CompanyDto(
            Guid id, 
            string name, 
            string email, 
            string phoneNumber, 
            string description, 
            List<string> photoUris, 
            List<int> categories,
            List<SocialMedia> socialMedias,
            string city, 
            string street, 
            bool isPrepaymentAvailable)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Description = description;
            PhotoUris = photoUris;
            Categories = categories;
            SocialMedias = socialMedias;
            City = city;
            Street = street;
            IsPrepaymentAvailable = isPrepaymentAvailable;
        }

        public Guid Id { get; }
        
        public string Name { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Description { get; }

        public List<string> PhotoUris { get; }

        public List<int> Categories { get; }
        
        public List<SocialMedia> SocialMedias { get; }

        public string City { get; }

        public string Street { get; }
        
        public bool IsPrepaymentAvailable { get; }
    }
}