using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(int id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);

        PersonVO Disable(int id);
        void Delete(int id);
    }
}
