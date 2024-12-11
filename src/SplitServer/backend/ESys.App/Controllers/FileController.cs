using ESys.Utilty.Defs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using IOFile = System.IO.File;

namespace ESys.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileController : Controller
    {
        private readonly ILogger<FileController> logger;
        private readonly IHttpContextAccessor accessor;

        public FileController(
            ILogger<FileController> logger,
            IHttpContextAccessor accessor
            )
        {
            this.logger = logger;
            this.accessor = accessor;
        }

        [HttpPost]
        public async Task<Result<string>> UploadImg(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            var tenant = this.HttpContext.GetTenantId();
            using var stream = file.OpenReadStream();
            var filePath = Path.GetFullPath(Path.Combine(Furion.App.Settings.VirtualPath, tenant, "img"));
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, fileName);
            using var writeStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            await stream.CopyToAsync(writeStream);
            return ResultBuilder.Ok(fileName);
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult Image([FromRoute] string name)
        {
            var tenant = this.HttpContext.GetTenantId();
            var filePath = Path.GetFullPath(Path.Combine(Furion.App.Settings.VirtualPath, tenant, "img", name));
            if (IOFile.Exists(filePath))
            {
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return this.File(stream, "application/octet-stream");
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}
