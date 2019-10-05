using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Models;
using Team11_CA.Shop.Core.ViewModels;

namespace Team11_CA.Shop.Core.Contracts
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems);
        List<MyPurchasesViewModel> GetPurchaseOrderSummary();
    }
}