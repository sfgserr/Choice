using Microsoft.AspNetCore.Mvc;

namespace FileObjectApi.Controllers
{
    [Route("api/objects")]
    [ApiController]
    public class FileController : Controller
    {
        private const long _maxFileSize = 1024*1024*2;

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
        public async Task<IActionResult> Upload(string fileName)
        {
            string path = $"{_path}/{fileName}";

            if (HttpContext.Request.ContentLength <= _maxFileSize)
            {
                byte[] data = new byte[(int)HttpContext.Request.ContentLength];

                await HttpContext.Request.Body.ReadAsync(data);

                if (!System.IO.File.Exists(path))
                {
                    await System.IO.File.WriteAllBytesAsync(path, data);
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
