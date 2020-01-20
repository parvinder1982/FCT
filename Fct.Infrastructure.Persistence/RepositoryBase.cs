namespace Fct.Infrastructure.Persistence
{
    using Fct.Infrastructure.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public class RepositoryBase<TObject> : IRepositoryBase<TObject> where TObject : class
    {
        private readonly FctDBContext context;

        private DbSet<TObject> dbSet => context.Set<TObject>();

        public IQueryable<TObject> Entities => dbSet;

        public RepositoryBase(FctDBContext dbContext)
        {
            context = dbContext;
        }

        public IQueryable<TObject> GetAll()
        {
            return dbSet.AsQueryable();
        }
        /// <inheritdoc/>
        public IQueryable<TObject> GetMany(Expression<Func<TObject, bool>> where)
        {
            return dbSet.Where(where).AsQueryable();
        }

        /// <inheritdoc/>
        public IQueryable<TObject> GetMany(Expression<Func<TObject, bool>> @where, params Expression<Func<TObject, object>>[] includes)
        {
            return dbSet.IncludeMultiple(includes).Where(where);
        }


        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Expression<Func<TObject, bool>> where)
        {
            return await dbSet.AnyAsync(where);

        }

        /// <inheritdoc/>
        public async Task<TObject> GetAsync(Expression<Func<TObject, bool>> where)
        {
            return await dbSet.FirstOrDefaultAsync(where);
        }

        /// <inheritdoc/>
        public async Task<TObject> GetAsync(Expression<Func<TObject, bool>> where, Expression<Func<TObject, object>> include)
        {
            return await dbSet.Include(include).FirstOrDefaultAsync(where);
        }

        public async Task<TObject> GetAsync(Expression<Func<TObject, bool>> where, Expression<Func<TObject, object>> include, Expression<Func<TObject, object>> include2)
        {
            return await this.dbSet.Include(include)
                .Include(include2)
                .FirstOrDefaultAsync(where);
        }

        /// <inheritdoc/>
        public void Update(TObject entity)
        {
            var entry = this.context.Entry(entity);
            this.dbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public void Create(TObject entity)
        {
            this.dbSet.Add(entity);
        }

        /// <inheritdoc/>
        public void Delete(TObject entity)
        {
            this.dbSet.Remove(entity);
        }
    }
}
