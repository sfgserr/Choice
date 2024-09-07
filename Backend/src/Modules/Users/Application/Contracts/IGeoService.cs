using Users.Domain.Users;

namespace Users.Application.Contracts
{
    public interface IGeoService
    {
        Task<string[]> GetCoords(string city, string street);
        
        Task<int> GetDistance(Coords coords1, Coords coords2);
    }
}
