using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string? _basePath;
        public readonly IHttpContextAccessor _context;
        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }
        public byte[] GetFile(string fileName)
        {
             var filePath = _basePath + fileName;
            return File.ReadAllBytes(filePath);
        }
        public async Task<FileDetailVo> SaveFileToDisk(IFormFile file)
        {
            FileDetailVo fileDetail = new FileDetailVo();
            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg" || fileType.ToLower() == ".txt")
            {
                var docName = Path.GetFileName(file.FileName);
                if(file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = docName;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "api/file/v1" + fileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return fileDetail;
        }
        public async Task<List<FileDetailVo>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVo> list = new List<FileDetailVo>();

            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }
            return list;
        }
    }
}
