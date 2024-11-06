namespace Administration.Application.Queries.GetClients
{
    public class ClientDto
    {
        public ClientDto(
            Guid id,
            string iconUri, 
            double averageGrade,
            string name, 
            string phoneNumber, 
            string email)
        {
            Id = id;
            IconUri = iconUri;
            AverageGrade = averageGrade;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
        public Guid Id { get; }
        
        public string IconUri { get; }
        
        public double AverageGrade { get; }

        public string Name { get; }

        public string PhoneNumber { get; }
        
        public string Email { get; }
    }
}