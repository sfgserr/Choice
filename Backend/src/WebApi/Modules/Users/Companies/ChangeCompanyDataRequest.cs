namespace WebApi.Modules.Users.Companies
{
    public class ChangeCompanyDataRequest
    {
        public ChangeCompanyDataRequest(
            string name,
            string email,
            string phoneNumber,
            string city,
            string street,
            string description,
            List<int> categories,
            List<string> photoUris,
            List<string> socialMediaUris,
            bool isPrepaymentAvailable)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
            Description = description;
            Categories = categories;
            PhotoUris = photoUris;
            SocialMediaUris = socialMediaUris;
            IsPrepaymentAvailable = isPrepaymentAvailable;
        }
        
        public string Name { get; }
        
        public string Email { get; }
        
        public string PhoneNumber { get; }
        
        public string City { get; }
        
        public string Street { get; }
        
        public string Description { get; }
        
        public List<int> Categories { get; }
        
        public List<string> PhotoUris { get; }

        public List<string> SocialMediaUris { get; }

        public bool IsPrepaymentAvailable { get; }
    }
}