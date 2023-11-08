using System.Threading.Tasks;

namespace Choice.Services.BlobServices
{
    public interface IBlobService
    {
        Task UploadPhoto(string fullPath);
    }
}
