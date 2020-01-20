
namespace Fct.Application.Services
{
    using Fct.Domain.Contracts.Infrastructure;
    using Fct.Infrastructure.Contracts;
    using Fct.Infrastructure.Persistence;
    using Fct.Infrastructure.Persistence.Services;
    using LightInject;
 

    /// <summary>
    ///     Implementation of LightInject's ICompositionRoot responsible for
    ///     registering all services required for the Application layer
    /// </summary>
    public class AppCompositionRoot : ICompositionRoot
    {
        /// <summary>
        ///     Called after LightInject ServiceContainer RegisterFor method is called
        /// </summary>
        /// <param name="serviceRegistry">LightInject's service registry</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterScoped<IUnitOfWork, UnitOfWork>();
            serviceRegistry.RegisterScoped<IUserService, UserService>();
            serviceRegistry.RegisterScoped<IProductItemService, ProductItemService>();
            serviceRegistry.RegisterScoped<IPurchaseService, PurchaseService>();
        }
    }
}
