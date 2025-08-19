namespace Administration.Application.Queries.GetClient
{
    public class ClientDto
    {
        public ClientDto(
            Guid id,
            string iconUri, 
            double averageGrade,
            string name, 
            string phoneNumber, 
            string email, 
            string city, 
            string street, 
            bool banned)
        {
            Id = id;
            IconUri = iconUri;
            AverageGrade = averageGrade;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            City = city;
            Street = street;
            Banned = banned;
        }
        
        public Guid Id { get; }
        
        public string IconUri { get; }
        
        public double AverageGrade { get; }

        public string Name { get; }

        public string PhoneNumber { get; }
        
        public string Email { get; }

        public string City { get; }

        public string Street { get; }
        
        public bool Banned { get; }
    }
}