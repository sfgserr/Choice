using Users.Domain.Users;

namespace Users.Application.Contracts
{
    public interface IDistanceService
    {
        int GetDistance(Coords coords1, Coords coords2);
    }
}
