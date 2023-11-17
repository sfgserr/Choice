using Choice.Domain.Models;
using System;
using Xamarin.Forms;

namespace Choice.ViewModels
{
    public class CategoryListViewModel
    {
        public CategoryListViewModel(Category category)
        {
            Id = category.Id;
            Title = category.Title.Replace('_', ' ');
            ImageSource = ImageSource.FromFile($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/{category.IconUri}.png");
        }

        public int Id { get; set; } 
        public string Title { get; set; }
        public ImageSource ImageSource { get; set; }
    }
}
