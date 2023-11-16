using SelectelSwiftApiClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Choice.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly string _token = "gAAAAABlVgWu8h9UfIl_z7QdBuSjjowiw0HkclKB6FCqHyuNrGIZwU4EPIh44286St7or02UeMqwjrC6JBA5uupp8RMloyH6Xbatt4S-3jUP-O7a6Vvoaxlj2j6kOPtbCniKvDLflpdxpXpjeuT9McTlE_Rw9Af0PjNdqHg2WdQn19mWb434Soo";
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
            await _client.Download(Path.GetFileName(fileName).Split('.').First(), fileName);
        }
    }
}
