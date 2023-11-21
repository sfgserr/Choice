using Choice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginByPhoneView : ContentView
    {
        public LoginByPhoneView()
        {
            InitializeComponent();
            BindingContext = ServicesContainer.GetService<LoginByPhoneViewModel>();
        }



        private void PhoneEntryFocused(object sender, FocusEventArgs e)
        {
            phoneFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void PhoneEntryUnfocused(object sender, FocusEventArgs e)
        {
            phoneFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void CodeEntryFocused(object sender, FocusEventArgs e)
        {
            codeFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void CodeEntryUnfocused(object sender, FocusEventArgs e)
        {
            codeFrame.BorderColor = Color.FromHex("#d5d5d7");
        }
    }
}