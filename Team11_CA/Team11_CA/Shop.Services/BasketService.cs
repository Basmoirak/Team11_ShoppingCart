using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.DataAccess.Repositories;
using Team11_CA.Shop.Core.Contracts;
using Team11_CA.Shop.Core.Models;
using Team11_CA.Shop.Core.ViewModels;

namespace Team11_CA.Shop.Services
{
    public class BasketService : IBasketService
    {
        ProductRepository productContext;
        BasketRepository basketContext;

        public const string BasketSessionName = "ShoppingBasket";

        public BasketService()
        {
            this.productContext = new ProductRepository();
            this.basketContext = new BasketRepository();
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            //Get cookie from client side, as it contains basketId for our reference
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if(cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Get(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else 
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }

            return basket;
        }

        //If cookie does not exist or there is no valid basketId
        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            //Create a new basket in the database
            Basket basket = new Basket();
            basketContext.Add(basket);
            basketContext.Commit();

            //Add basketId into the cookie
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(7);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productId, string quantity)
        {
            //Retrieve basket from the database
            Basket basket = GetBasket(httpContext, true);

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
        public void UpdateBasket(HttpContextBase httpContext, string productId, string quantity)
        {
            //Retrieve basket from the database
            Basket basket = GetBasket(httpContext, true);

            //Retrieve product from the basket if it exists
            BasketItem item = basket.BasketItems.FirstOrDefault(x => x.Id == productId);

            item.Quantity = int.Parse(quantity);
            
            basketContext.Commit();
        }
        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(x => x.Id == itemId);

            if(item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);

            //If Basket exists, return basketItems to the viewmodel
            //Else, return a new empty list of basketItems to the viewmodel
            if(basket != null)
            {
                var result = (from b in basket.BasketItems
                              join p in productContext.GetAll() on b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
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

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
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
        public void ClearBasket(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            basket.BasketItems.Clear();
            basketContext.Commit();
        }
    }
}