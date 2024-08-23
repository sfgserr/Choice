
namespace BuildingBlocks.Application.Contracts
{
    public interface IGeoService
    {
        Task<string[]> GetCoords(string city, string street);
    }
}
