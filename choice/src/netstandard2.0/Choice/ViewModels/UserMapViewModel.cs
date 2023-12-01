using Choice.Domain.Models;
using System;
using System.IO;

namespace Choice.ViewModels
{
    public class UserMapViewModel
    {
        public UserMapViewModel(User user) 
        {
            SetIconStream(user);
        }

        public Stream IconStream { get; set; }

        private void SetIconStream(User user)
        {
            using (FileStream stream = new FileStream($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/{user.IconUri}.png", FileMode.Open))
            {
                IconStream = stream;
            }
        }
    }
}
