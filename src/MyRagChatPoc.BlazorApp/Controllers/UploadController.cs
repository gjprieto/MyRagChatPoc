using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRagChatPoc.Kernel.Services.FileCache;

namespace MyRagChatPoc.BlazorApp.Controllers
{
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("upload/single")]
        public IActionResult Single(IFormFile file, [FromServices] IFileCacheService fileCache)
        {
            try
            {
                var memStream = new MemoryStream();
                file.OpenReadStream().CopyTo(memStream);
                fileCache.StoreFile(file.FileName, memStream);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/multiple")]
        public IActionResult Multiple(IFormFile[] files, [FromServices] IFileCacheService fileCache)
        {
            try
            {
                foreach (var file in files)
                {
                    var memStream = new MemoryStream();
                    file.OpenReadStream().CopyTo(memStream);
                    fileCache.StoreFile(file.FileName, memStream);
                }

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/{id}")]
        public IActionResult Post(IFormFile[] files, int id)
        {
            try
            {
                // Put your code here
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
