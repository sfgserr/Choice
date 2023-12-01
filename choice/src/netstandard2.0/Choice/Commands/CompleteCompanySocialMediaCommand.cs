using Choice.Domain.Models;
using Choice.Stores.IndexStores;
using Choice.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Choice.Commands
{
    public class CompleteCompanySocialMediaCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly CompanySocialMediasViewModel _viewModel;
        private readonly IIndexStore _indexStore;

        public CompleteCompanySocialMediaCommand(CompanySocialMediasViewModel viewModel, IIndexStore indexStore)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += OnCanExecuteChanged;

            _indexStore = indexStore;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanCompleteSocialMedias;
        }

        public void Execute(object parameter)
        {
            _indexStore.State++;

            List<SocialMedia> socialMedias = new List<SocialMedia>()
            {
                new SocialMedia() { Title = "Instagram", Uri = _viewModel.InstagramUri },
                new SocialMedia() { Title = "Facebook", Uri = _viewModel.FacebookUri },
                new SocialMedia() { Title = "Vk", Uri = _viewModel.VkUri },
                new SocialMedia() { Title = "Telegram", Uri = _viewModel.TelegramUri },
            };

            _viewModel.Input.SocialMedias = socialMedias;
        }

        private void OnCanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.CanCompleteSocialMedias)) CanExecuteChanged?.Invoke(sender, e);
        }
    }
}
