using BuildingBlocks.Application.Cqrs.Commands;
using BuildingBlocks.Application.Extensions;
using Users.Application.Contracts;
using Users.Domain.OrderRequests.OrderResponses;
using Users.Domain.Users;

namespace Users.Application.OrderResponses.Commands.Enroll
{
    internal class EnrollCommandHandler : ICommandHandler<EnrollCommand>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IPaymentService _paymentService;
        
        internal EnrollCommandHandler(IUsersDbContext dbContext, IUserContext userContext, IPaymentService paymentService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _paymentService = paymentService;
        }

        public async Task Execute(EnrollCommand command)
        {
            var response = await _dbContext.OrderResponses.Get(r => 
                r.Id.Equals(new OrderResponseId(command.ResponseId)));
            
            await response.Enroll(_userContext.ClientId, _paymentService);
        }
    }
}