namespace BuildingBlocks.Application.GeoCoding
{
    public interface IGeoCodingService
    {
        Task<string[]> GetCoords(string city, string street);
    }
}