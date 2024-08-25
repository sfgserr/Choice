using BuildingBlocks.Application;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BuildingBlocks.Infrastructure.Authorization
{
    public class CustomClaimsTransformation : IClaimsTransformation
    {
        private readonly IModule _module;

        public CustomClaimsTransformation(IModule module)
        {
            _module = module;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            
        }
    }
}
