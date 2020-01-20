namespace Fct.Infrastructure.Persistence.Services
{
    using Fct.Domain.Contracts.Infrastructure;
    using Fct.Infrastructure.Contracts;
    using Fct.Infrastructure.Persistence.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using System;

    public class ProductItemService : IProductItemService
    {
        private readonly IRepositoryBase<Customer> customerRepository;
        private readonly IRepositoryBase<Product> productRepository;
        private readonly IRepositoryBase<Purchase> purchaseRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public ProductItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.customerRepository = unitOfWork.CustomerRepository;
            this.productRepository = unitOfWork.ProductRepository;
            this.purchaseRepository = unitOfWork.PurchaseRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<Domain.Models.Product> GetProductsAsync()
        {
            try {
             
                var products = this.productRepository.GetAll()
                    .Select(b => new Domain.Models.Product
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Price = b.Price
                    });
                
                return products;
            } 
            catch(Exception ex)
            {
                // catch block
                Console.WriteLine(ex.Message);
            }
            return null; 
        }

        public async Task<Domain.Models.Product> GetProductAsync(int productId)
        {
            var product = await this.productRepository.GetAsync(p => p.Id == productId);
            return mapper.Map<Domain.Models.Product>(product);
        }
    }
}
