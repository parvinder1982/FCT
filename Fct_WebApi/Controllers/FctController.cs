using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fct.Domain.Contracts.Application;
using Fct.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fct_WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FctController : ControllerBase
    {
        private readonly ICustomerService customerService;
             private readonly IProductService productService;

        public FctController(ICustomerService customerService, IProductService productService)
        {
            this.customerService = customerService;
                 this.productService = productService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await customerService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("userproducts")]
        public async Task<IActionResult> GetUserProducts([FromBody]int userId)
        {
            var userProducts = await customerService.GetUserProductsAsync(userId);

            if (userProducts == null)
                return BadRequest(new { message = "Product/s are not available for user" });

            return Ok(userProducts);
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            var products = await productService.GetProductsAsync();

            if (products == null)
                return BadRequest(new { message = "Products are not available, Please coordinate with FCT administrator" });

            return Ok(products);
        }
        [AllowAnonymous]
        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseProduct([FromBody]PurchaseProduct purchaseProduct)
        {
            var isProductPurchased = await customerService.BuyProductAsync(purchaseProduct);

            if (!isProductPurchased)
               return BadRequest(new { message = "Unable to purchase this Product, Please coordinate with FCT administrator" });

            return Ok(isProductPurchased);
        }
        [AllowAnonymous]
        [HttpPost("cancelproduct")]
        public async Task<IActionResult> CancelProduct([FromBody]PurchaseProduct purchaseProduct)
        {
            var isProductCancelled = await customerService.CancelProductAsync(purchaseProduct);

            if (!isProductCancelled)
                return BadRequest(new { message = "Unable to cancel the Product, Please coordinate with FCT administrator" });

            return Ok(isProductCancelled);
        }
    }
}
