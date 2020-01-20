using Fct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fct.Domain.Contracts.Application
{
    public interface ICustomerService
    {
        Task<User> AuthenticateAsync(string username, string password);

        Task<IEnumerable<Domain.Models.Product>> GetUserProductsAsync(int userId);

        Task<bool> BuyProductAsync(PurchaseProduct purchaseProduct);

        Task<bool> CancelProductAsync(PurchaseProduct purchaseProduct);
    }
}
