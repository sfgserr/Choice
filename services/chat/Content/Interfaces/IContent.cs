namespace Choice.Chat.Api.Content.Interfaces
{
    public interface IContent
    {
        void ChangeContent(Action<object> action);

        bool Match(string propertyName, object value);

        object Body { get; }

        event Action<object> BodyChanged;
    }
}
