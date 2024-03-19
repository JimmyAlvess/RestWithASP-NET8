using RestWithASPNET.Models;

namespace RestWithASPNETErudio.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);
        Book FindByID(int id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(int id);
    }
}
