using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;
using RestWithASPNET.Models;

namespace RestWithASPNET.Repository
{
    public interface IPersonRepository : IRepository<Person>
    { 
        Person Disabled (int id);
        List<Person> FindByName(string firstName, string secondName);
    }
}