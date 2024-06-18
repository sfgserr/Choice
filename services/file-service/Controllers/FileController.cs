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
                byte[] data = await System.IO.File.ReadAllBytesAsync(path);
                return File(data, "image/png");
            }

            return NotFound();
        }

        [HttpPost("{fileName}")]
        public async Task<IActionResult> Upload(string fileName)
        {
            byte[] data = new byte[150000];

            await HttpContext.Request.Body.ReadAsync(data);

            string path = $"{_path}/{fileName}";

            if (!System.IO.File.Exists(path))
            {
                await System.IO.File.WriteAllBytesAsync(path, data);
                return Ok();
            }

            return BadRequest();
        }
    }
}
