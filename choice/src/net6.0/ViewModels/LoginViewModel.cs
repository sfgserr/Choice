using Choice.Commands;
using System.Windows.Input;

namespace Choice.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            DisplayActionSheetCommand = new DisplayAccountCreationActionSheet();
        }

        public ICommand DisplayActionSheetCommand { get; }
    }
}
