
namespace Choice.Dialogs.LinkSocialMediaDialogs
{
    public interface ILinkSocialMediaDialogService
    {
        Task ShowDialogAsync(string text, Action<string> action);
    }
}
