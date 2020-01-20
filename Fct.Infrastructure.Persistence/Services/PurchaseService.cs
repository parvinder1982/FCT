using Fct.Domain.Contracts.Infrastructure;
using Fct.Domain.Models;
using Fct.Infrastructure.Contracts;
using Fct.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fct.Infrastructure.Persistence.Services
{
   public class PurchaseService : IPurchaseService
    {

        private readonly IRepositoryBase<Customer> customerRepository;
        private readonly IRepositoryBase<Purchase> purchaseRepository;
        private readonly IRepositoryBase<Entities.Product> productRepository;
        private readonly IUnitOfWork unitOfWork;


        public PurchaseService(IUnitOfWork unitOfWork)
        {
            this.customerRepository = unitOfWork.CustomerRepository;
            this.purchaseRepository = unitOfWork.PurchaseRepository;
            this.productRepository = unitOfWork.ProductRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> PlacePurchaseOrderAsync(PurchaseProduct purchaseProduct)
        {
            var isIteamAlreadyPurchased = await this.purchaseRepository.ExistsAsync( p => p.ProductId == purchaseProduct.ProductId && p.UserId == purchaseProduct.UserId);
            if (!isIteamAlreadyPurchased)
            {
                this.purchaseRepository.Create(new Purchase { ProductId = purchaseProduct.ProductId, UserId = purchaseProduct.UserId });
                await this.unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CancelPurchaseOrderAsync(PurchaseProduct purchaseProduct)
        {
            var product = await this.purchaseRepository.GetAsync(p => p.ProductId == purchaseProduct.ProductId && p.UserId == purchaseProduct.UserId);
            if (product != null)
            {
                this.purchaseRepository.Delete(product);
                await this.unitOfWork.CommitAsync();
                return true;
            }
            return false;
        } 
    }
}
