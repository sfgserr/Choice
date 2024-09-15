namespace Identity.Application.Authorization.GetUser
{
    public class UserDto
    {
        public UserDto(
            string roleCode, 
            string city, 
            string street, 
            string latitude, 
            string longitude)
        {
            RoleCode = roleCode;
            City = city;
            Street = street;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string RoleCode { get; }

        public string City { get; }

        public string Street { get; }

        public string Latitude { get; }

        public string Longitude { get; }

        public List<string> Permissions { get; } = [];
    }
}