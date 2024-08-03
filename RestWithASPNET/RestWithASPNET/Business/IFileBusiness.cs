using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVo> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVo>>SaveFilesToDisk(IList<IFormFile> file);
    }
}
