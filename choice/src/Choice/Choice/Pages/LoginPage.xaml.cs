using Choice.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<LoginViewModel>();
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

        private void PhoneEntryFocused(object sender, FocusEventArgs e)
        {
            phoneFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void PhoneEntryUnfocused(object sender, FocusEventArgs e)
        {
            phoneFrame.BorderColor = Color.FromHex("#d5d5d7");
        }
    }
}