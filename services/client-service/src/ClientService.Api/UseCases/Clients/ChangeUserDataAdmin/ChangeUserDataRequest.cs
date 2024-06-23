namespace Choice.ClientService.Api.UseCases.Clients.ChangeUserDataAdmin
{
    public class ChangeUserDataAdminRequest
    {
        public ChangeUserDataAdminRequest(string id, string name, string surname, string email, string phoneNumber, 
            string city, string street) 
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }

        public string Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string City { get; }
        public string Street { get; }
    }
}
