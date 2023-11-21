using System;
using System.Threading.Tasks;

namespace Choice.Dialogs.LinkSocialMediaDialogs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LinkSocialMediaDialog
	{
        private readonly Func<bool, string, Task> _clicked;

        public LinkSocialMediaDialog(string text, Func<bool, string, Task> clicked)
        {
            InitializeComponent();

            Text.Text = $"Ссылка на ваш {text}";
            _clicked = clicked;
        }

        private void EntryFocused(object sender, FocusEventArgs e)
        {
            frame.BorderColor = Color.FromHex("#3F8AE0");
        }

        private void EntryUnfocused(object sender, FocusEventArgs e)
        {
            frame.BorderColor = Color.FromHex("#d5d5d7");
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            _clicked(true, Uri.Text);
        }

        private void CloseButton_Clicked(object sender, System.EventArgs e)
        {
            _clicked(true, "");
        }
    }
}