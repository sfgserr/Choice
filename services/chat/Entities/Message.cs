namespace Choice.Chat.Api.Entities
{
    public class Message
    {
        public Message(string text, string senderId, string receiverId)
        {
            Text = text;
            SenderId = senderId;
            ReceiverId = receiverId;
        }

        public int Id { get; private set; }
        public string Text { get; private set; }
        public string SenderId { get; private set; }
        public string ReceiverId { get; private set; }

        public void EditText(string text) =>
            Text = text;
    }
}
