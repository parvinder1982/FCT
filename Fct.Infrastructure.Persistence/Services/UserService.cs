using AutoMapper;
using Fct.Domain.Contracts.Infrastructure;
using Fct.Domain.Models;
using Fct.Infrastructure.Contracts;
using Fct.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product = Fct.Infrastructure.Persistence.Entities.Product;

namespace Fct.Infrastructure.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<Customer> customerRepository;
        private readonly IProductItemService productItemService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
       
        public UserService(IProductItemService productItemService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.productItemService = productItemService;
            this.customerRepository = unitOfWork.CustomerRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<User> GetUserAsync(string userName, string password)
        {
            try
            {
                var userEntity = await this.customerRepository.GetAsync(u => u.Name == userName && u.Password == password, p => p.Purchase);

                if (userEntity != null)
                {
                    return new User()
                    {
                        UserId = userEntity.Id,
                        Username = userEntity.Name,
                        Password = userEntity.Password,
                        Email = userEntity.Email,
                        Token = string.Empty
                    };
                }
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Domain.Models.Product>> GetUserProductsAsync(int userId)
        {
            try
            {
                var userEntity = await this.customerRepository.GetAsync(u => u.Id == userId, p => p.Purchase);

                if (userEntity != null)
                {
                    var userPurchasedItems = userEntity?.Purchase.ToList();

                    if(userPurchasedItems != null)
                    {
                        var userProducts = this.productItemService.GetProductsAsync().SelectMany(a => userPurchasedItems?.Where(b => b.ProductId == a.Id)
                                                                            .Select(b => new Domain.Models.Product
                                                                            {
                                                                                Id = b.ProductId,
                                                                                Name = a.Name,
                                                                                Price = a.Price
                                                                            })).ToList();

                        return userProducts;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

    }
}
