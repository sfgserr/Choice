using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;

namespace Chat.Application.ChatUsers.Commands.AddOrUpdateDevice
{
    internal class AddOrUpdateDeviceCommandHandler : ICommandHandler<AddOrUpdateDeviceCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IChatDbContext _dbContext;

        internal AddOrUpdateDeviceCommandHandler(IUserContext userContext, IChatDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task Execute(AddOrUpdateDeviceCommand command)
        {
            var user = await _dbContext.ChatUsers.Get(u => u.Id.Equals(_userContext.Id));
            
            user.AddOrUpdateDevice(command.DeviceName, command.DeviceToken);
        }
    }
}