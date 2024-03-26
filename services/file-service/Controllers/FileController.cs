using Microsoft.AspNetCore.Mvc;

namespace FileObjectApi.Controllers
{
    [Route("api/objects")]
    [ApiController]
    public class FileController : Controller
    {
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            string path = $"/etc/files/{fileName}.bin";

            byte[] data = await System.IO.File.ReadAllBytesAsync(path);

            return File(data, "image/png");
        }

        [HttpPost("{fileName}")]
        public async Task<IActionResult> Upload(string fileName, [FromBody] byte[] data)
        {
            string path = $"/etc/files/{fileName}.bin";

            await System.IO.File.WriteAllBytesAsync(path, data);

            return Ok();
        }
    }
}
