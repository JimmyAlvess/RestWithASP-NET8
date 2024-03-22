using RestWithASPNETErudio.Data.VO;

namespace RestWithASPNETErudio.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(int id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO person);
        void Delete(int id);
    }
}
