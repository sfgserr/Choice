using Choice.Domain.Models;
using System.IO;

namespace Choice.ViewModels
{
    public class UserMapViewModel
    {
        public UserMapViewModel(User user) 
        {

        }

        public Stream IconStream { get; set; }

        private void SetIconStream(User user)
        {
            using (FileStream stream = FileStream())
        }
    }
}
