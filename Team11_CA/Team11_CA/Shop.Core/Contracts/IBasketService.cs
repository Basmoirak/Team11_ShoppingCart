using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.ViewModels;

namespace Team11_CA.Shop.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(string productId, string quantity);
        List<BasketItemViewModel> GetBasketItems();
        BasketSummaryViewModel GetBasketSummary();
        void UpdateBasket(string productId, string quantity);
        void RemoveFromBasket(string itemId);
        void ClearBasket();
    }
}