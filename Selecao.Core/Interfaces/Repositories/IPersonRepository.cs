using Selecao.Core.Entities;

namespace Selecao.Core.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        public void Save();

        public void Update(Person person);

    }
}
