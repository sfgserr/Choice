using SelectelSwiftApiClient;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;

namespace Choice.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly string _token = "gAAAAABlV4xpPLgpIcSHZmK4bckkG0aGO_zJX2qJr9pI55VpO-zYG42_LaFNMDm2zEmJvNH2CvATgczlWhNQpvrH_JUZDIqyJ6HdIKAgbt2JhA7Ot-3Bu7Lx095R_cejMKx0_vMrm5pXVeux_Dw9Onqcim6xK_dADZX3OfowlZiAPu7YeKEygoQ";
        private readonly string _projectId = "76a36decdef041c684850fdf4ae2258a";

        private readonly ContainerClient _client;
        
        public FileService()
        {
            _client = new ContainerClient(_token, _projectId, "choicecontainer");
        }

        public async Task UploadPhoto(string fullPath)
        {
            await _client.Upload(fullPath);
        }

        public async Task DownloadPhoto(string fileName)
        {
            await _client.Download(Path.GetFileName(fileName).Split('.').First(), $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/{fileName}");
        }
    }
}
