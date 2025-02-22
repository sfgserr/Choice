namespace Identity.Application.Authorization.GetUser
{
    public class UserDto
    {
        public UserDto(
            string roleCode, 
            string city, 
            string street, 
            string latitude, 
            string longitude, 
            bool isSubscribed)
        {
            RoleCode = roleCode;
            City = city;
            Street = street;
            Latitude = latitude;
            Longitude = longitude;
            IsSubscribed = isSubscribed;
        }

        public string RoleCode { get; }

        public string City { get; }

        public string Street { get; }

        public string Latitude { get; }

        public string Longitude { get; }
        
        public bool IsSubscribed { get; }

        public List<string> Permissions { get; } = [];
    }
}