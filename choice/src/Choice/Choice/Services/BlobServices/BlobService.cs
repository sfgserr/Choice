using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Choice.Services.BlobServices
{
    public class BlobService : IBlobService
    {
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=choiceweb;AccountKey=QCIBNkxIiKkJhlwmUkwjpFl9fDZjubqUSiTAQB4t/b6QEqiIyUz1xWLRUz5r/ifRiRLwriNZZOOq+AStGe0LGQ==;EndpointSuffix=core.windows.net";
        private readonly string _containerName = "icons0568f1ff-9f0c-44cd-8438-c591fdb4a147";

        private readonly BlobContainerClient _containerClient;

        public BlobService()
        {
            _containerClient = new BlobContainerClient(_connectionString, _containerName);
        }

        public async Task UploadPhoto(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
                return;

            BlobClient client = _containerClient.GetBlobClient(fullPath.Split('/').Last());

            using (Stream file = File.OpenRead(fullPath))
            {
                await client.UploadAsync(file);
            }
        }

        public async Task DownloadPhoto(string fileName)
        {
            BlobClient client = _containerClient.GetBlobClient(fileName);

            string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            using (Stream file = File.OpenWrite(directory))
            {
                await client.DownloadToAsync(file);
            }
        }
    }
}
