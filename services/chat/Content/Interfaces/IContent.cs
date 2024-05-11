namespace Choice.Chat.Api.Content.Interfaces
{
    public interface IContent<T> where T : class
    {
        T GetContent();

        IContent<T> ChangeContent(Action<T> action);

        string Content { get; }
    }
}
