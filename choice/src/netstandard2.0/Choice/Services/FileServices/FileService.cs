using SelectelSwiftApiClient;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Choice.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly string _token = "gAAAAABlW0wgOEle-zlgvTp5jz9rLVjWe-hPGCh1i8_TcoMbODUbxRCsqwuphr4N8uf9_1f0u-1E7_UnoIuXJ7JIAkVEFoL0M8Y-4tiDQVBzYvCE_RUot2AUWw_ml-jLodkzAnqgUooygDkMzlgZcoyqecn6Lm0_0r3KYl2qNHF93Uvg9lfnVPk";
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
