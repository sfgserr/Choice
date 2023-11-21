using System;
using System.Threading.Tasks;

namespace Choice.Dialogs.AccountCreatedDialogs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountCreatedDialog
	{
		private readonly Func<bool, Task> _callBack;

        public AccountCreatedDialog(string title, string message, string buttonText, Func<bool, Task> callBack)
        {
            InitializeComponent();
            _callBack = callBack;
            label1.Text = title;
            label2.Text = message;
            btn.Text = buttonText;
        }

        private async void BtOk_Clicked(object sender, EventArgs e)
        {
            await _callBack.Invoke(true);
        }
    }
}