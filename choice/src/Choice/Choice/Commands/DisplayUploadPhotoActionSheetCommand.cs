using Choice.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Choice.Commands
{
    class DisplayUploadPhotoActionSheetCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly CompanyDescriptionViewModel _viewModel;

        public DisplayUploadPhotoActionSheetCommand(CompanyDescriptionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            int index = int.Parse(parameter.ToString());

            string action = await Application.Current.MainPage.DisplayActionSheet("Выбрать фото", "Отмена", null,
                                                                                  "Сделать фото", "Выбрать из галлереи",
                                                                                  "Выбрать файл");

            switch (action)
            {
                case "Отмена":
                    return;
                case "Сделать фото":
                    await PickPhoto(index);
                    break;

            }
        }

        private async Task PickPhoto(int index)
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();
            _viewModel.PhotoViewModels[index].Source = ImageSource.FromFile(photo.FullPath);
        }
    }
}
