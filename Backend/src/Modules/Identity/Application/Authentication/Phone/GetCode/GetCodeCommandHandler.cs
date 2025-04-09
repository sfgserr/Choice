using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Exceptions;
using BuildingBlocks.Application.Extensions;
using Identity.Application.Contracts;
using Identity.Domain.Users;

namespace Identity.Application.Authentication.Phone.GetCode
{
    internal class GetCodeCommandHandler : ICommandHandler<GetCodeCommand>
    {
        private readonly IIdentityDbContext _dbContext;
        private readonly ISmsService _smsService;
        
        internal GetCodeCommandHandler(IIdentityDbContext dbContext, ISmsService smsService)
        {
            _dbContext = dbContext;
            _smsService = smsService;
        }

        public async Task Execute(GetCodeCommand command)
        {
            var user = _dbContext.Users.TranslatedWhere<User, UserDataModel>(u => 
                u.PhoneNumber == command.PhoneNumber).FirstOrDefault();
            InvalidCommandException.ThrowIfNull(user);
            
            await _smsService.SendSmsAsync(command.PhoneNumber, CodeStore.Add(user.Id.Value));
        }
    }
}