
namespace Choice.ClientService.Infrastructure.Geolocation
{
    public sealed class AddressServiceOptions
    {
        public AddressServiceOptions(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string ApiKey { get; }
    }
}
