using Fct.Domain.Contracts;
using Fct.Domain.Contracts.Application;
using Fct.Domain.Contracts.Infrastructure;
using Fct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fct.Application.Services
{
   public  class ProductService : IProductService
    {
        private readonly IProductItemService productItemService;
        public ProductService(IProductItemService productItemService)
        {
            this.productItemService = productItemService;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return this.productItemService.GetProductsAsync();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await this.productItemService.GetProductAsync(productId);
        }
    }
}
