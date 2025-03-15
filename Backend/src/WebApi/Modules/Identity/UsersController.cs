using Identity.Application.Contracts;
using Identity.Application.Users.ChangePassword;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Identity
{
    [Route("api/identity")]
    public class UsersController : Controller
    {
        private readonly IIdentityModule _identityModule;

        public UsersController(IIdentityModule identityModule)
        {
            _identityModule = identityModule;
        }

        [HttpPut]
        [HasPermission(Permissions.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            await _identityModule.ExecuteCommand(new ChangePasswordCommand(
                request.OldPassword,
                request.NewPassword));
            
            return Ok();
        }
    }
}