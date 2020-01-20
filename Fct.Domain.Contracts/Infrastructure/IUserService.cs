using Fct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fct.Domain.Contracts.Infrastructure
{
    public interface IUserService
    {
      Task<User> GetUserAsync(string userName, string password);

      Task<IEnumerable<Product>> GetUserProductsAsync(int userId);

    }
}
