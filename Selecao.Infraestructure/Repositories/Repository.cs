using Microsoft.EntityFrameworkCore;
using Selecao.Core.Interfaces.Repositories;
using System.Linq.Expressions;



namespace Selecao.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SelecaoContext _db;
        internal DbSet<T> _dbSet;

        public Repository(SelecaoContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return query.ToList();
        }
        
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }


    }
}
