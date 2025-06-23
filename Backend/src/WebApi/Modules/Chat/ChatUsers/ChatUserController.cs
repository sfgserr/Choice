using Chat.Application.ChatUsers.Commands.AddOrUpdateDevice;
using Chat.Application.ChatUsers.Commands.RemoveDevice;
using Chat.Application.Contracts;
using Identity.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Modules.Chat.ChatUsers
{
    [ApiController]
    [Route("api/chatUsers")]
    public class ChatUserController : Controller
    {
        private readonly IChatModule _chatModule;

        public ChatUserController(IChatModule chatModule)
        {
            _chatModule = chatModule;
        }

        [HttpPut]
        [HasPermission(Permissions.AddOrUpdateDevice)]
        public async Task<IActionResult> AddOrUpdateDevice([FromBody] AddOrUpdateDeviceRequest request)
        {
            await _chatModule.ExecuteCommand(new AddOrUpdateDeviceCommand(request.Device, request.Token));
            
            return Ok();
        }
        
        [HttpDelete("{device}")]
        [HasPermission(Permissions.RemoveDevice)]
        public async Task<IActionResult> RemoveDevice(string device)
        {
            await _chatModule.ExecuteCommand(new RemoveDeviceCommand(device));
            
            return Ok();
        }
    }
}