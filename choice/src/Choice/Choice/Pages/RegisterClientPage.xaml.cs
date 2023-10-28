using Choice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterClientPage : ContentPage
    {
        public RegisterClientPage()
        {
            InitializeComponent();

            BindingContext = ServicesContainer.GetService<RegisterClientViewModel>();
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

        private void SurnameEntryFocused(object sender, FocusEventArgs e)
        {
            surnameFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void SurnameEntryUnfocused(object sender, FocusEventArgs e)
        {
            surnameFrame.BorderColor = Color.FromHex("#d5d5d7");
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