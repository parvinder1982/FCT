using System;
using System.Collections.Generic;
using System.Text;

namespace Fct.Infrastructure.Persistence
{
    using Fct.Infrastructure.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using Fct.Infrastructure.Persistence.Entities;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FctDBContext dbContext;

        public UnitOfWork(FctDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UnitOfWork(IConfiguration configuration)
        {
            this.dbContext = new FctDBContext(configuration);
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
        }

        public void RejectChanges()
        {
            var changedEntries = dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();
            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        #region Repositories

        public IRepositoryBase<Customer> CustomerRepository => new RepositoryBase<Customer>(this.dbContext);

        public IRepositoryBase<Product> ProductRepository => new RepositoryBase<Product>(this.dbContext);

        public IRepositoryBase<Purchase> PurchaseRepository => new RepositoryBase<Purchase>(this.dbContext);

        #endregion
    }
}
