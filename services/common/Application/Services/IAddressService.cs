using Choice.Common.ValueObjects;

namespace Choice.Application.Services
{
    public interface IAddressService
    {
        Task<int> GetDistance(Address clientAddress, Address companyAddress);

        Task<string> Geocode(Address address);
    }
}
