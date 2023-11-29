using FileObjectClient;

namespace Choice.Services.FileServices
{
    public class FileService : IFileService
    {
        //private readonly string _projectId = "76a36decdef041c684850fdf4ae2258a";

        //private readonly SelectelObjectClient _objectClient;
        //private readonly ContainerClient _client;

        private readonly ObjectClient _client;

        public FileService()
        {
            _client = new ObjectClient();
        }

        public async Task UploadPhoto(string fullPath)
        {
            string fileName = Path.GetFileName(fullPath).Split(".").First();
            await _client.Upload(fileName, fullPath);
        }

        public async Task DownloadPhoto(string fileName)
        {
            await _client.Download(fileName.Split('.').First(), $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/{fileName}");
        }
    }
}
