using Fct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fct.Domain.Contracts.Infrastructure
{
    public interface IProductItemService
    {
       IEnumerable<Product> GetProductsAsync();

       Task<Product> GetProductAsync(int productId);
    }
}
