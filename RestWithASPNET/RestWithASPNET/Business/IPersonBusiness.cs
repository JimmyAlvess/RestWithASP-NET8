
using RestWithASPNETErudio.Model;

namespace RestWithASPNETErudio.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);
        Person FindByID(int id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(int id);
    }
}
