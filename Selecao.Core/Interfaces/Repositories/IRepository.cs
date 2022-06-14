using System.Linq.Expressions;


namespace Selecao.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {

        public void Add(T entity);

        public IEnumerable<T> GetAll();

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter);

        public void Remove(T entity);

        public void RemoveRange(IEnumerable<T> entities);


    }
}
