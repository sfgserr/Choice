using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Chat.Application.Contracts;
using Chat.Domain.ChatUsers;

namespace Chat.Application.ChatUsers.Commands.RemoveDevice
{
    internal class RemoveDeviceCommandHandler : ICommandHandler<RemoveDeviceCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IChatDbContext _dbContext;
        
        internal RemoveDeviceCommandHandler(IUserContext userContext, IChatDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task Execute(RemoveDeviceCommand command)
        {
            var user = await _dbContext.ChatUsers.Get(c => c.Id.Equals(_userContext.Id));
            
            user.RemoveDevice(command.Name);
        }
    }
}