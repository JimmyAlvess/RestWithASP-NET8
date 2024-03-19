using RestWithASPNET.Models;
using RestWithASPNET.Models.Base;

namespace RestWithASPNET.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindByID(int  id);
        List<T> FindAll();
        T Update(T item);
        void Delete(int id);
        bool Exists(int id);
    }
}
