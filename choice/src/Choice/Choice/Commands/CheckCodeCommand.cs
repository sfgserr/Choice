using Choice.ViewModels;
using System;
using System.Linq;
using System.Windows.Input;
using Twilio.Rest.Verify.V2.Service;

namespace Choice.Commands
{
    public class CheckCodeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly LoginViewModel _viewModel;

        public CheckCodeCommand(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string phoneNumber = new string(_viewModel.PhoneNumber.Where(c => char.IsDigit(c)).ToArray());

            var verificationCheck = VerificationCheckResource.Create(to: phoneNumber, code: _viewModel.Code, pathServiceSid: "VAXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");

            //TODO: if status approved navigate to main page
        }
    }
}
