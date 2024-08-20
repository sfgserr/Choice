using BuildingBlocks.Domain;

namespace Chat.Domain.ChatUsers
{
    public class ChatUserId : TypedIdValueBase
    {
        public ChatUserId(Guid value) : base(value)
        {

        }
    }
}
