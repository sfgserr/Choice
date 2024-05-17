﻿
namespace Choice.Chat.Api.ViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel(string name, string iconUri, string lastMessage, string guid, bool isRead, 
            DateTime sentTime, bool isUserReceiver)
        {
            Name = name;
            IconUri = iconUri;
            LastMessage = lastMessage;
            Guid = guid;
            IsRead = isRead;
            SentTime = sentTime;
            IsUserReceiver = isUserReceiver;
        }

        public string Name { get; }
        public string IconUri { get; }
        public string LastMessage { get; }
        public string Guid { get; }
        public bool IsRead { get; }
        public DateTime SentTime { get; }
        public bool IsUserReceiver { get; }
    }
}
