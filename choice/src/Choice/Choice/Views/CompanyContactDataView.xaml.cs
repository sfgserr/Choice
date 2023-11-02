using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyContactDataView : ContentView
    {
        public CompanyContactDataView()
        {
            InitializeComponent();
        }

        private void EmailEntryFocused(object sender, FocusEventArgs e)
        {
            emailFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void EmailEntryUnfocused(object sender, FocusEventArgs e)
        {
            emailFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void TitleEntryFocused(object sender, FocusEventArgs e)
        {
            titleFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void TitleEntryUnfocused(object sender, FocusEventArgs e)
        {
            titleFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void PhoneEntryFocused(object sender, FocusEventArgs e)
        {
            phoneFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void PhoneEntryUnfocused(object sender, FocusEventArgs e)
        {
            phoneFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void SiteEntryFocused(object sender, FocusEventArgs e)
        {
            siteFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void SiteEntryUnfocused(object sender, FocusEventArgs e)
        {
            siteFrame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void AddressEntryFocused(object sender, FocusEventArgs e)
        {
            addressFrame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void AddressEntryUnfocused(object sender, FocusEventArgs e)
        {
            addressFrame.BorderColor = Color.FromHex("#d5d5d7");
        }
    }
}