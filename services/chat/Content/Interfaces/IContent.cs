namespace Choice.Chat.Api.Content.Interfaces
{
    public interface IContent
    {
        object GetContent();

        void ChangeContent(Func<object, string> action);

        bool Match(string propertyName, object value);

        string Body { get; }

        event Action<string> BodyChanged;
    }
}
