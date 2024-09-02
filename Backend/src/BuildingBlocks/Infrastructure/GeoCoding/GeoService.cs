using BuildingBlocks.Application.GeoCoding;

namespace BuildingBlocks.Infrastructure.GeoCoding
{
    public class GeoService : IGeoService
    {
        public async Task<string[]> GetCoords(string city, string street) =>
            await Task.Run<string[]>(() => ["55.749531", "37.591352"]);
    }
}
