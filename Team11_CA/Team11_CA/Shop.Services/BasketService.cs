using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.DataAccess.Repositories;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.Shop.Services
{
    public class BasketService
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

        public void AddToBasket(HttpContextBase httpContext, string productId)
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
                    Quantity = 1
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + 1;
            }

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
    }
}