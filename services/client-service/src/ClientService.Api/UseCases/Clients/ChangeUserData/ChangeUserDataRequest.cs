namespace Choice.ClientService.Api.UseCases.Clients.ChangeUserData
{
    public class ChangeUserDataRequest
    {
        public ChangeUserDataRequest(string name, string surname, string email, string phoneNumber, string city,
            string street) 
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            City = city;
            Street = street;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string City { get; }
        public string Street { get; }
    }
}
