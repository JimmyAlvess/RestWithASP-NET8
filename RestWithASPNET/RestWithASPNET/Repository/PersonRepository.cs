using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        { }
        public Person Disabled(int id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return user;
        }

        public List<Person> FindByName(string firstName, string lastname)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastname))
            {
                return _context.Persons.Where(
                     p => p.FirstName.Contains(firstName)
                     && p.LastName.Contains(lastname)).ToList();
            }
            else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastname))
            {
                return _context.Persons.Where(
                     p => p.LastName.Contains(lastname)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastname))
            {
                return _context.Persons.Where(
                     p => p.FirstName.Contains(firstName)).ToList();
            }
            return null;
        }
    }
}
