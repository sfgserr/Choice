namespace Choice.Chat.Api.Entities
{
    public class Message
    {
        public Message(string text, string senderId, int groupId)
        {
            Text = text;
            SenderId = senderId;
            GroupId = groupId;
        }

        public int Id { get; private set; }
        public string Text { get; private set; }
        public string SenderId { get; private set; }
        public int GroupId { get; private set; }
        public DateTime CreationTime { get; private set; } = DateTime.Now;
    }
}
