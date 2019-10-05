using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Contracts;
using Team11_CA.Shop.Core.Models;
using Team11_CA.Shop.Core.ViewModels;
using Team11_CA.Shop.DataAccess.Repositories;

namespace Team11_CA.Shop.Services
{
    public class OrderService : IOrderService
    {
        OrderRepository orderContext;

        public OrderService()
        {
            this.orderContext = new OrderRepository();
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach(var item in basketItems)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    baseOrder.OrderItems.Add(new OrderItem()
                    {
                        ProductID = item.Id,
                    });
                }

                orderContext.Add(baseOrder);
            }

            orderContext.Commit();
        }
    }
}