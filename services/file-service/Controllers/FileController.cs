using Microsoft.AspNetCore.Mvc;

namespace FileObjectApi.Controllers
{
    [Route("api/objects")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly string _path;

        public FileController(IConfiguration configuration)
        {
            _path = configuration["FILE_UPLOAD_PATH"] ?? "etc/files";
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            string path = $"{_path}/{fileName}";

            if (System.IO.File.Exists(path))
            {
                string extension = path.Split('-').Last();

                byte[] data = await System.IO.File.ReadAllBytesAsync(path);
                return File(data, $"image/{extension}");
            }

            return NotFound();
        }

        [HttpPost("{fileName}")]
        public async Task<IActionResult> Upload(string fileName, [FromBody] IFormFile file)
        {
            string path = $"{_path}/{fileName}";

            using var stream = new MemoryStream();

            using var fs = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);

            if (stream.Length <= 2097152)
            {
                stream.WriteTo(fs);
                return Ok();
            }

            return BadRequest();
        }
    }
}
