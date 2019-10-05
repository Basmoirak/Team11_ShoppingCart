using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.DataAccess.Repositories;
using Team11_CA.Shop.Core.Contracts;
using Team11_CA.Shop.Core.Models;
using Team11_CA.Shop.Core.ViewModels;
using Team11_CA.Shop.Core.Library;

namespace Team11_CA.Shop.Services
{
    public class BasketService : IBasketService
    {
        ProductRepository productContext;
        BasketRepository basketContext;

        public BasketService()
        {
            this.productContext = new ProductRepository();
            this.basketContext = new BasketRepository();
        }

        private Basket GetBasket(bool createIfNull)
        {
            Basket basket = basketContext.GetBasketFromUserID(HttpContext.Current.Session["UserID"].ToString());

            if(basket == null)
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket();
                }
            }

            return basket;
        }
        private Basket CreateNewBasket()
        {
            //Create a new basket in the database
            Basket basket = new Basket();
            basketContext.Add(basket);
            basketContext.Commit();

            return basket;
        }
        public void AddToBasket(string productId, string quantity)
        {
            //Retrieve basket from the database
            Basket basket = GetBasket(true);

            //Retrieve product from the basket if it exists
            BasketItem item = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);

            //Add product into the basket if product does not exist, else increment product quantity
            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantity = int.Parse(quantity)
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + int.Parse(quantity);
            }

            basketContext.Commit();
        }
        public void UpdateBasket(string basketId, string quantity)
        {
            //Retrieve basket from the database
            Basket basket = GetBasket(true);

            //Retrieve product from the basket if it exists
            BasketItem item = basket.BasketItems.FirstOrDefault(x => x.Id == basketId);

            if(item != null)
            {
                item.Quantity = int.Parse(quantity);
                basketContext.Commit();
            }
        }
        public void RemoveFromBasket(string basketId)
        {
            Basket basket = GetBasket(true);
            BasketItem item = basket.BasketItems.FirstOrDefault(x => x.Id == basketId);

            if(item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
        public List<BasketItemViewModel> GetBasketItems()
        {
            Basket basket = GetBasket(false);

            //If Basket exists, return basketItems to the viewmodel
            //Else, return a new empty list of basketItems to the viewmodel
            if(basket != null)
            {
                var result = (from b in basket.BasketItems
                              join p in productContext.GetAll() on b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  BasketId = b.Id,
                                  Quantity = b.Quantity,
                                  ProductId = p.Id,
                                  ProductName = p.Name,
                                  Image = p.Image,
                                  Price = p.Price
                              }).ToList();

                return result;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }
        public BasketSummaryViewModel GetBasketSummary()
        {
            Basket basket = GetBasket(false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);

            if(basket != null)
            {
                //If there are basket items, return the total quantity. Else, return null
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();

                //If there are basket items, return the total sum of the basket. Else, return null
                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.GetAll() on item.ProductId equals p.Id
                                        select item.Quantity * p.Price).Sum();

                //If BasketCount or BasketTotal is null, return zero. Else, return value.
                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return model;
            }
        }
        public void ClearBasket()
        {
            Basket basket = GetBasket(false);
            basket.BasketItems.Clear();
            basketContext.Commit();
        }
        public int CheckBasketQuantity()
        {
            Basket basket = GetBasket(false);
            return basket.BasketItems.Count();
        }
    }
}