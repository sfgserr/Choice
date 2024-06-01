using Choice.Common.ValueObjects;

namespace Choice.Application.Services
{
    public interface IAddressService
    {
        Task<int> GetDistance(string clientCoords, string companyCoords);

        Task<string> Geocode(Address address);
    }
}
