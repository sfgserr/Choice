using Choice.ViewModels;

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

            if (e.Value)
                viewModel.LinkSocialMediaCommand.Execute("Instagram");
            else
                viewModel.InstagramUri = string.Empty;
        }

        private void OnFacebookToggled(object sender, ToggledEventArgs e)
        {
            CompanySocialMediasViewModel viewModel = (CompanySocialMediasViewModel)BindingContext;

            if (e.Value)
                viewModel.LinkSocialMediaCommand.Execute("Facebook");
            else
                viewModel.FacebookUri = string.Empty;
        }

        private void OnVkToggled(object sender, ToggledEventArgs e)
        {
            CompanySocialMediasViewModel viewModel = (CompanySocialMediasViewModel)BindingContext;

            if (e.Value)
                viewModel.LinkSocialMediaCommand.Execute("Vk");
            else
                viewModel.VkUri = string.Empty;
        }

        private void OnTelegramToggled(object sender, ToggledEventArgs e)
        {
            CompanySocialMediasViewModel viewModel = (CompanySocialMediasViewModel)BindingContext;

            if (e.Value)
                viewModel.LinkSocialMediaCommand.Execute("Telegram");
            else
                viewModel.TelegramUri = string.Empty;
        }
    }
}