namespace WebApi.Seed
{
    public class ClientsOption
    {
        public List<Client> Clients { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}