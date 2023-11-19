using System.Threading.Tasks;

namespace Choice.Services.AddressServices
{
    public interface IAddressService
    {
        Task<string> GetAddressByCoordinates(double latitude, double longtitude);
    }
}
