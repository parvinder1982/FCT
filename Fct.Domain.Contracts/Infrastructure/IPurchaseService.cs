using Fct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fct.Domain.Contracts.Infrastructure
{
    public interface IPurchaseService
    {
        Task<bool> PlacePurchaseOrderAsync(PurchaseProduct purchaseProduct);

        Task<bool> CancelPurchaseOrderAsync(PurchaseProduct purchaseProduct);
    }
}
