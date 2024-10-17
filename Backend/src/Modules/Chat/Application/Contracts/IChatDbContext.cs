using Microsoft.EntityFrameworkCore;
using Chat.Domain.ChatUsers;
using Chat.Domain.Messages;

namespace Chat.Application.Contracts
{
    public interface IChatDbContext
    {
        DbSet<ChatUser> ChatUsers { get; }

        DbSet<Message> Messages { get; }
    }
}
