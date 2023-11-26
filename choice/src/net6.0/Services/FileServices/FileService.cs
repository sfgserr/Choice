using SelectelClient.Clients;

namespace Choice.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly string _projectId = "76a36decdef041c684850fdf4ae2258a";

        private readonly SelectelObjectClient _objectClient;
        private readonly ContainerClient _client;
        
        public FileService()
        {
            _objectClient = new SelectelObjectClient();
            _client = _objectClient.CreateContainerClient(_projectId, "choicecontainer");
        }

        public async Task UploadPhoto(string fullPath)
        {
            await _client.Upload(fullPath);
        }

        public async Task DownloadPhoto(string fileName)
        {
            await _client.Download(fileName.Split('.').First(), $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/{fileName}");
        }
    }
}
