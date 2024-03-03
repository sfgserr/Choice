using Choice.ClientService.Domain.ClientAggregate;

namespace Choice.ClientService.Application.Services
{
    public interface IAddressService
    {
        Task<int> GetDistance(Address clientAddress, Address companyAddress);
    }
}
