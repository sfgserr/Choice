using Choice.Commands;
using Choice.Stores.Navigators;
using Choice.ViewModels.Factories;
using System.Windows.Input;

namespace Choice.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly INavigator _navigator;

    public MainViewModel(INavigator navigator, IViewModelFactory factory)
    {
        _navigator = navigator;
        _navigator.StateChanged += OnCurrentViewChanged;

        UpdateCurrentViewCommand = new UpdateCurrentViewCommand(factory, _navigator);
        UpdateCurrentViewCommand.Execute(ViewType.Login);
    }

    public ICommand UpdateCurrentViewCommand { get; }
    public ViewModelBase CurrentView => _navigator.State;

    private void OnCurrentViewChanged()
    {
        OnPropertyChanged(nameof(CurrentView));
    }
}
