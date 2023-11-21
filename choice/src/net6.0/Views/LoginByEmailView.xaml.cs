using Choice.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginByEmailView : ContentView
    {
        public LoginByEmailView()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<LoginByEmailViewModel>();
        }

        private void EmailEntryFocused(object sender, FocusEventArgs e)
        {
            emailFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void EmailEntryUnfocused(object sender, FocusEventArgs e)
        {
            emailFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void PasswordEntryFocused(object sender, FocusEventArgs e)
        {
            passwordFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void PasswordEntryUnfocused(object sender, FocusEventArgs e)
        {
            passwordFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

    }
}