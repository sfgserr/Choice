namespace WebApi.Modules.Admin.Users
{
    public class EditCompanyRequest
    {
        public EditCompanyRequest(
            Guid id, 
            string iconUri,
            string name, 
            string description, 
            string email, 
            string phoneNumber, 
            string city, 
            string street, 
            string[] socialMedias, 
            int[] categoryIds, 
            string[] photoUris, 
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

        public string[] SocialMedias { get; }

        public int[] CategoryIds { get; }

        public string[] PhotoUris { get; }

        public bool IsPrepaymentAvailable { get; }
    }
}