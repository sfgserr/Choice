namespace BuildingBlocks.Application.Authentication
{
    public interface IUserService
    {
        Guid GetUserId();
        
        string GetAttribute(string city);
    }
}
