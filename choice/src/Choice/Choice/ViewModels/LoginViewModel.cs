using Choice.Commands;
using Choice.Extensions;
using Choice.Stores.Authenticators;
using System.Linq;
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
