using System.Threading.Tasks;

namespace Choice.Services.FileServices
{
    public interface IFileService
    {
        Task UploadPhoto(string fullPath);

        Task DownloadPhoto(string fileName);
    }
}
