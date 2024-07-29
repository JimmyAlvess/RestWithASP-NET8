using Microsoft.AspNetCore.Components.Web;
using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Hypermedia.Utils;
using RestWithASPNET.Model;
using RestWithASPNET.Repository;

namespace RestWithASPNET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }
        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string? name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (pageSize - 1) * size : 0;

            string query = @"SELECT * FROM Person p WHERE 1 = 1 ";
            if (!String.IsNullOrWhiteSpace(name)) query += $"AND p.first_name LIKE '%{name}%' ";
            query += $"ORDER BY p.last_name {sort} OFFSET {offset} ROWS FETCH NEXT {size} ROWS ONLY;";

            string countQuery = "SELECT  COUNT(*)  FROM Person p WHERE 1 = 1";
            if (!String.IsNullOrWhiteSpace(name)) countQuery = countQuery + $"AND p.first_name LIKE '%{name}%'";

            var persons = _repository.FindWithPageSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO>
            {
                CurrenPage = page,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sortDirection,
                TotalResult = totalResults,
            };
        }
        public PersonVO FindByID(int id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disabled(id);
            return _converter.Parse(personEntity);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
