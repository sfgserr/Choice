using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Infrastructure.Authentication
{
    public class UserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid GetUserId()
        {
            string? id = _contextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

            if (id is not null)
            {
                bool isParsed = Guid.TryParse(id, out Guid guid);

                if (isParsed)
                    return guid;

                throw new ArgumentException("User id is not guid");
            }

            throw new ApplicationException("User context in unavailable");
        }
    }
}
