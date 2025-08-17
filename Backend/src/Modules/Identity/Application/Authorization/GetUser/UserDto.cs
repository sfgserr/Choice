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
            bool isSubscribed, bool banned)
        {
            RoleCode = roleCode;
            City = city;
            Street = street;
            Latitude = latitude;
            Longitude = longitude;
            IsSubscribed = isSubscribed;
            Banned = banned;
        }

        public string RoleCode { get; }

        public string City { get; }

        public string Street { get; }

        public string Latitude { get; }

        public string Longitude { get; }
        
        public bool IsSubscribed { get; }
        
        public bool Banned { get; }

        public List<string> Permissions { get; } = [];
    }
}