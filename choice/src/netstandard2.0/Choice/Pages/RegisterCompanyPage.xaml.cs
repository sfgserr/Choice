using Choice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCompanyPage : ContentPage
    {
        public RegisterCompanyPage()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<RegisterCompanyViewModel>();
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

        private void PasswordConfirmtionEntryFocused(object sender, FocusEventArgs e)
        {
            passwordConfirmtionFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void PasswordConfirmtionEntryUnfocused(object sender, FocusEventArgs e)
        {
            passwordConfirmtionFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void NameEntryFocused(object sender, FocusEventArgs e)
        {
            nameFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void NameEntryUnfocused(object sender, FocusEventArgs e)
        {
            nameFrame.BorderColor = Color.FromHex("#d5d5d7");
        }
    }
}