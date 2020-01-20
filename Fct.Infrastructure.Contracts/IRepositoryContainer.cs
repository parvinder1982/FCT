

namespace Fct.Infrastructure.Contracts
{
    using Fct.Infrastructure.Persistence.Entities;
    public interface IRepositoryContainer
    {
        IRepositoryBase<Customer> CustomerRepository { get; }
        IRepositoryBase<Product> ProductRepository { get; }
        IRepositoryBase<Purchase> PurchaseRepository { get; }
      
    }
}
