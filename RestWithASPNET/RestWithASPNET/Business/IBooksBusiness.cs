using RestWithASPNET.Data.VO;
using RestWithASPNET.Models;

namespace RestWithASPNETErudio.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO BookVO);
        BookVO FindByID(int id);
        List<BookVO> FindAll();
        BookVO Update(BookVO BookVO);
        void Delete(int id);
    }
}
