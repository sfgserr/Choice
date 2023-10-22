using Choice.ViewModels;

namespace Choice.Stores.Navigators
{
    public enum ViewType
    {
        Login
    }

    public interface INavigator : IStore<ViewModelBase> { }
}
