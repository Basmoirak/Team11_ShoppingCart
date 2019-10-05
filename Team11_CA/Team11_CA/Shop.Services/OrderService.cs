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
        ProductRepository productContext;
        Repository<OrderItem> orderItemContext;

        public OrderService()
        {
            this.orderContext = new OrderRepository();
            this.productContext = new ProductRepository();
            this.orderItemContext = new Repository<OrderItem>();
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach(var item in basketItems)
            {
                //Create a new OrderItem for each product in the basket, to generate activation code
                for (int i = 0; i < item.Quantity; i++)
                {
                    baseOrder.OrderItems.Add(new OrderItem()
                    {
                        ProductID = item.ProductId,
                    });
                }

                orderContext.Add(baseOrder);
            }

            orderContext.Commit();
        }
        private IEnumerable<Order> GetCustomerOrders()
        {
            string customerId = HttpContext.Current.Session["UserID"].ToString();
            IEnumerable<Order> orders = orderContext.GetAll().Where(order => order.CustomerID == customerId);

            return orders;
        }
        private IEnumerable<String> GetActivationCodeList(string productId, DateTime dateCreated)
        {
            IEnumerable<OrderItem> orderItems = orderItemContext.GetAll();
            IEnumerable<Order> orders = GetCustomerOrders();

            IEnumerable<String> activationCodes = (from o in orders
                                               join i in orderItems on o.Id equals i.OrderID
                                               where i.DateCreated == dateCreated && 
                                                     i.ProductID == productId
                                               select i.ActivationCode.ToString());

            return activationCodes;
        }
        //my changes
        public List<MyPurchasesViewModel> GetPurchaseOrderSummaryByOrderId(string orderId)
        {
            string customerId = HttpContext.Current.Session["UserID"].ToString();
            List<MyPurchasesViewModel> model = new List<MyPurchasesViewModel>();
            IEnumerable<Order> orders = orderContext.GetAll();
            IEnumerable<Product> products = productContext.GetAll();

            //Group DateCreated and ProductID together
            var query = orders.Where(order => order.CustomerID == customerId && order.Id == orderId )
                  .Select(order => order.OrderItems
                  .GroupBy(item => new
                  {
                      item.DateCreated,
                      item.ProductID
                  }).Select(grp => new
                  {
                      DateCreated = grp.Key.DateCreated,
                      ProductID = grp.Key.ProductID,
                      Quantity = grp.Count()
                  }).ToList());

            //Add entire customer order history into the model
            foreach (var order in query)
            {
                foreach (var item in order)
                {
                    model.Add(new MyPurchasesViewModel
                    {
                        OrderCreatedDate = item.DateCreated.ToString("dd - MMMM - yyyy"),
                        OrderQuantity = item.Quantity,
                        ActivationCodes = GetActivationCodeList(item.ProductID, item.DateCreated),
                        ProductDescription = productContext.GetProductDescription(item.ProductID, products),
                        ProductImage = productContext.GetProductImage(item.ProductID, products),
                        ProductName = productContext.GetProductName(item.ProductID, products)
                    });
                }
            }

            return model;
        }
        public List<MyPurchasesViewModel> GetPurchaseOrderSummary()
        {
            string customerId = HttpContext.Current.Session["UserID"].ToString();
            List<MyPurchasesViewModel> model = new List<MyPurchasesViewModel>();
            IEnumerable<Order> orders = orderContext.GetAll();
            IEnumerable<Product> products = productContext.GetAll();

            //Group DateCreated and ProductID together
            var query = orders.Where(order => order.CustomerID == customerId)
                  .Select(order => order.OrderItems
                  .GroupBy(item => new
                  {
                      item.DateCreated,
                      item.ProductID
                  }).Select(grp => new
                  {
                      DateCreated = grp.Key.DateCreated,
                      ProductID = grp.Key.ProductID,
                      Quantity = grp.Count()
                  }).ToList());

            //Add entire customer order history into the model
            foreach (var order in query)
            {
                foreach(var item in order)
                {
                    model.Add(new MyPurchasesViewModel
                    {
                        OrderCreatedDate = item.DateCreated.ToString("dd - MMMM - yyyy"),
                        OrderQuantity = item.Quantity,
                        ActivationCodes = GetActivationCodeList(item.ProductID, item.DateCreated),
                        ProductDescription = productContext.GetProductDescription(item.ProductID, products),
                        ProductImage = productContext.GetProductImage(item.ProductID, products),
                        ProductName = productContext.GetProductName(item.ProductID, products)
                    });
                }
            }

            return model;
        }
    }
}