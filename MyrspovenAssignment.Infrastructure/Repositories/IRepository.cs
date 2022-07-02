using System.Linq.Expressions;

namespace MyrspovenAssignment.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T AddIfNotExists(T entity, Expression<Func<T, bool>> predicate);
        T Update(T entity);
        T Get(Guid id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
