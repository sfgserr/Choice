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
            byte[] data = new byte[15000];

            await HttpContext.Request.Body.ReadAsync(data);

            string path = $"/etc/files/{fileName}.bin";

            if (!System.IO.File.Exists(path))
            {
                await System.IO.File.WriteAllBytesAsync(path, data);
                return Ok();
            }

            return BadRequest();
        }
    }
}
