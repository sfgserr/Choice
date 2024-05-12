namespace Choice.Chat.Api.Content.Interfaces
{
    public interface IContent
    {
        object GetContent();

        void ChangeContent(Action<object> action);

        bool Match(string propertyName, object value);

        string Content { get; }
    }
}
