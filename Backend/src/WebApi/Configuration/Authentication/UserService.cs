using BuildingBlocks.Application.Authentication;

namespace WebApi.Configuration.Authentication
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetUserId()
        {
            var id = GetAttribute("id");

            var result = Guid.TryParse(id, out var parsedId);

            return result ? parsedId : throw new ArgumentException("User id is not guid");
        }

        public string GetAttribute(string attributeName)
        {
            var attributeValue = _contextAccessor.HttpContext?.User?.FindFirst(attributeName)?.Value;

            if (attributeValue is null)
                throw new ApplicationException("User context is unavailable");

            return attributeValue;
        }
    }
}
