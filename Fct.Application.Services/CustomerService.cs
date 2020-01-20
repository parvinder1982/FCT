using Fct.Domain.Contracts.Application;
using Fct.Domain.Contracts.Infrastructure;
using Fct.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fct.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly IPurchaseService purchaseService;
        public CustomerService(IUserService userService, IPurchaseService purchaseService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
            this.purchaseService = purchaseService;
        }
        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            var user = await this.userService.GetUserAsync(userName, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration["Tokens:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(this.configuration["Tokens:TokenExpiryInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = string.Empty;

            return user;
        }

        public async Task<IEnumerable<Product>> GetUserProductsAsync(int userId)
        {
            return await this.userService.GetUserProductsAsync(userId);
        }
        public async Task<bool> BuyProductAsync(PurchaseProduct purchaseProduct)
        {
            return  await this.purchaseService.PlacePurchaseOrderAsync(purchaseProduct);
        }

        public async Task<bool> CancelProductAsync(PurchaseProduct purchaseProduct)
        {
            return await this.purchaseService.CancelPurchaseOrderAsync(purchaseProduct); ;
        }
    }
}
