using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http;
using System.Text;
using WebApp.Helpers;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class AssetController : Controller
    {
        private readonly ILogger<AssetController> _logger;
        private readonly DirectusService _directusService;

        public AssetController(ILogger<AssetController> logger, DirectusService directusService)
        {
            _logger = logger;
            _directusService = directusService;
        }

        [HttpGet]
        public async Task<ActionResult> Get(string id, int bytes = 0, int download = 0)
        {
            var asset = await _directusService.GetFileAssetAsync(id);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = asset.Filename_Disk,
                Inline = true,
            };

            var fileBytes = (bytes == 1) ?
                await _directusService.GetFileContentAsync(id, HttpContext.Request.Query) :
                Encoding.UTF8.GetBytes(await _directusService.GetFileTextAsync(id));

            if (download == 1)
                return File(fileBytes, asset.Type, asset.Filename_Download);
            else
                return File(fileBytes, asset.Type);
        }
    }
}
