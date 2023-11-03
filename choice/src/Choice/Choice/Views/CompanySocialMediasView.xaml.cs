using Choice.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Choice.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanySocialMediasView : ContentView
    {
        public CompanySocialMediasView()
        {
            InitializeComponent();
        }

        private void OnInstagramToggled(object sender, ToggledEventArgs e)
        {
            CompanySocialMediasViewModel viewModel = (CompanySocialMediasViewModel)BindingContext;
            viewModel.LinkSocialMediaCommand.Execute("Instagram");
        }

        private void OnFacebookToggled(object sender, ToggledEventArgs e)
        {
            CompanySocialMediasViewModel viewModel = (CompanySocialMediasViewModel)BindingContext;
            viewModel.LinkSocialMediaCommand.Execute("Facebook");
        }

        private void OnVkToggled(object sender, ToggledEventArgs e)
        {
            CompanySocialMediasViewModel viewModel = (CompanySocialMediasViewModel)BindingContext;
            viewModel.LinkSocialMediaCommand.Execute("Vk");
        }

        private void OnTelegramToggled(object sender, ToggledEventArgs e)
        {
            CompanySocialMediasViewModel viewModel = (CompanySocialMediasViewModel)BindingContext;
            viewModel.LinkSocialMediaCommand.Execute("Telegram");
        }
    }
}