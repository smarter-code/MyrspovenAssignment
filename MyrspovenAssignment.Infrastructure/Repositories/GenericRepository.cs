using System.Linq.Expressions;

namespace MyrspovenAssignment.Infrastructure.Repositories
{
    public abstract class GenericRepository<T>
         : IRepository<T> where T : class
    {
        protected MyrspovenAssignmentContext context;

        public GenericRepository(MyrspovenAssignmentContext context)
        {
            this.context = context;
        }

        public virtual T Add(T entity)
        {
            return context
                .Add(entity)
                .Entity;
        }

        public virtual T AddIfNotExists(T entity, Expression<Func<T, bool>> predicate = null)
        {
            var exists = Find(predicate).FirstOrDefault() != null ? true : false;
            if (!exists)
                return Add(entity);
            return entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual T Get(Guid id)
        {
            return context.Find<T>(id);
        }

        public virtual IEnumerable<T> All()
        {
            return context.Set<T>()
                .ToList();
        }

        public virtual T Update(T entity)
        {
            return context.Update(entity)
                .Entity;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
