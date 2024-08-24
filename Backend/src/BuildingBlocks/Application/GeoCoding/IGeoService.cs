namespace BuildingBlocks.Application.GeoCoding
{
    public interface IGeoService
    {
        Task<string[]> GetCoords(string city, string street);
    }
}
