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
        private readonly string _token = "gAAAAABlWcYfYSPfBmyeCKhpW5tBHuaLSZYM-VSpLTe00hEOX6x6OZear1V-WFYzD-iTmYqrIDXYfT1qA-129D9IKk5oUeEfr7Ez0TsSTkFuON9T2vrLNfCZMKL6lMNywznJTdPe_plFX9B1ye09RL5IxIZQ2LwgnU2rtmPbE0aQo6cmYFhquH8";
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
