using BuildingBlocks.Application.Cqrs.Commands;

namespace Chat.Application.Messages.Commands.Read
{
    public class ReadCommand : ICommand
    {
        public ReadCommand(Guid messageId)
        {
            MessageId = messageId;
        }

        public Guid MessageId { get; }
    }
}