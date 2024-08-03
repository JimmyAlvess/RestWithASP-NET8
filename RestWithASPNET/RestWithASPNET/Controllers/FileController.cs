using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RestWithASPNET.Business;
using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{verson:apiVersion}")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;
        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }
        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);
            if(buffer != null)
            {
                HttpContext.Response.ContentType =
                    $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }
        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVo))]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetailVo detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }
        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType((200), Type = typeof(List<FileDetailVo>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(4001)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFile([FromForm] List<IFormFile> files)
        {
            List<FileDetailVo> details = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }
    }
}
