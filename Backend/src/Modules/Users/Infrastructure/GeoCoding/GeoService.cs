using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Infrastructure.GeoCoding
{
    internal class GeoService : IGeoService
    {
        public async Task<string[]> GetCoords(string city, string street) =>
            await Task.Run<string[]>(() => ["55.749531", "37.591352"]);

        public async Task<int> GetDistance(Coords coords1, Coords coords2) =>
            await Task.Run(() => 250);
    }
}
