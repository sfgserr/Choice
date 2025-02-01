using BuildingBlocks.Application.Cqrs.Commands;
using Chat.Application.Contracts;
using Users.Application.Contracts;
using Users.Application.OrderResponses.Queries.GetOrderResponse;

namespace Chat.Application.Chat.Commands.SendOrderMessage
{
    internal class SendOrderMessageCommandHandler : ICommandHandler<SendOrderMessageCommand>
    {
        private readonly IUsersModule _usersModule;
        private readonly IChatService _chatService;
        
        internal SendOrderMessageCommandHandler(IUsersModule usersModule, IChatService chatService)
        {
            _usersModule = usersModule;
            _chatService = chatService;
        }

        public async Task Execute(SendOrderMessageCommand command)
        {
            var orderResponse = await _usersModule.Query<GetOrderResponseQuery, OrderResponseDto>(
                new GetOrderResponseQuery(command.ResponseId));

            await _chatService.SendOrder(orderResponse, command.ToUserId);
        }
    }
}