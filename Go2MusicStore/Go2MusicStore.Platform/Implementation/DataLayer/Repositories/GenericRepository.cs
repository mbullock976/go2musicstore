using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go2MusicStore.Platform.Implementation.DataLayer.Repositories
{
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Go2MusicStore.Platform.Interfaces.DataLayer.Repositories;

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /*
         * The code Expression<Func<TEntity, bool>> filter means the caller will provide a lambda expression
         *  based on the TEntity type, and this expression will return a Boolean value. 
         *  For example, if the repository is instantiated for the Student entity type, 
         *  the code in the calling method might specify student => student.LastName == "Smith" 
         *  for the filter parameter. 
            The code Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy also means the caller 
            will provide a lambda expression. But in this case, the input to the expression is an 
            IQueryable object for the TEntity type. The expression will return an ordered version of 
            that IQueryable object. For example, if the repository is instantiated for the Student 
            entity type, the code in the calling method might specify q => q.OrderBy(s => s.LastName) 
            for the orderBy parameter.
            The Get method uses lambda expressions to allow the calling code to specify a filter condition 
            and a column to order the results by, and a string parameter lets the caller provide a 
            comma-delimited list of navigation properties for eager loading
         * */
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        //Overloaded Due to Unit Tests
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity GetByID(object[] ids)
        {
            return dbSet.Find(ids);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
