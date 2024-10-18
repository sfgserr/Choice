using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : ICommand
    {
        public CreateMessageCommand(string content, Guid toUserId, string type)
        {
            Content = content;
            ToUserId = toUserId;
            Type = type;
        }

        public string Content { get; }

        public Guid ToUserId { get; }

        public string Type { get; }
    }
}
