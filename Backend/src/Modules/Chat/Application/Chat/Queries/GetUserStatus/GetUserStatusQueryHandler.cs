using BuildingBlocks.Application.Cqrs.Queries;
using Chat.Application.Contracts;

namespace Chat.Application.Chat.Queries.GetUserStatus
{
    internal class GetUserStatusQueryHandler : IQueryHandler<GetUserStatusQuery, bool>
    {
        private readonly IChatUsersStore _usersStore;

        internal GetUserStatusQueryHandler(IChatUsersStore usersStore)
        {
            _usersStore = usersStore;
        }

        public async Task<bool> Handle(GetUserStatusQuery query)
        {
            int count = 15;
            int checkInterval = 1000;
            
            while (!_usersStore.IsUserOnline(query.UserId))
            {
                if (count == 0)
                    return false;
                
                await Task.Delay(checkInterval);
                count--;
            }

            return true;
        }
    }
}