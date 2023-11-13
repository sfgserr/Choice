using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Choice.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly string _baseUri = "https://swift.ru-1.storage.selcloud.ru/v1/76a36decdef041c684850fdf4ae2258a/choicecontainer/";
        private readonly string _authToken = "gAAAAABlUi5Xn0uKD_8Pb8VMhAnr3741LNB9mY0APhEzHsQgn1qaM-ao0pJK7BM58wtBEXFC5cgb9DuoRV5UzncAcGHDj5ODqFdfVn0c01osr2thA07Tv3p-o3FyAYm_hd_e6-QcLoEjtabKPVWt6_BLnAGh2UmCBEhAw2-oeVIZDNBQN2K0QDk";

        private readonly HttpClient _client;

        public FileService(HttpClient client)
        {
            _client = client;
        }

        public async Task UploadPhoto(string fullPath)
        {
            string[] directories = fullPath.Split(new[] { '/', '.' });

            string requestUri = $"{_baseUri}{directories[directories.Length - 2]}";

            string json = string.Join("", File.ReadAllBytes(fullPath));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = new StringContent(json)
            };
            request.Headers.Add("X-Auth-Token", _authToken);

            await _client.SendAsync(request);
        }

        public async Task DownloadPhoto(string fileName)
        {
            string requestUri = $"{_baseUri}{fileName}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("X-Auth-Token", _authToken);

            HttpResponseMessage response = await _client.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();

            byte[] data = json.Select(c => byte.Parse(c.ToString())).ToArray();

            File.WriteAllBytes(fileName, data);
        }
    }
}
