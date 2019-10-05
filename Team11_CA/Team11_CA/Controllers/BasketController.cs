using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_CA.Shop.Core.Filters;
using Team11_CA.Shop.Core.Models;
using Team11_CA.Shop.Core.ViewModels;
using Team11_CA.Shop.Services;

namespace Team11_CA.Controllers
{
    [LoginFilter]
    public class BasketController : Controller
    {
        BasketService basketService;
        OrderService orderService;

        public BasketController()
        {
            this.basketService = new BasketService();
            this.orderService = new OrderService();
        }

        // GET: Basket
        public ActionResult Index()
        {
            List<BasketItemViewModel> model = basketService.GetBasketItems();

            return View(model);
        }

        public ActionResult AddToBasket(string Id, string quantity)
        {
            basketService.AddToBasket(Id, quantity);

            return RedirectToAction("Index","Home");
        }
        public ActionResult UpdateBasket(string Id, string quantity)
        {
            basketService.UpdateBasket(Id, quantity);

            return RedirectToAction("Index", "Basket");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(Id);

            return RedirectToAction("Index");
        }
        public ActionResult ClearBasket()
        {
            basketService.ClearBasket();

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            BasketSummaryViewModel basketSummary = basketService.GetBasketSummary();

            return PartialView(basketSummary);
        }

        public ActionResult Checkout()
        {
            Order order = new Order();
            var basketItems = basketService.GetBasketItems();
            int basketItemQty = basketService.CheckBasketQuantity();

            //If the basket is empty
            if(basketItemQty < 1)
            {
                return RedirectToAction("Index","Basket");
            }
            else
            {
                //Set Order Status to Completed
                order.OrderStatus = "Order Completed";

                //Create Order and clear Shopping Cart Basket
                orderService.CreateOrder(order, basketItems);
                basketService.ClearBasket();

                return RedirectToAction("MyPurchases", new { OrderId = order.Id });
            }

        }
        public ActionResult MyPurchases()
        {
            List<MyPurchasesViewModel> model = new List<MyPurchasesViewModel>();
            model = orderService.GetPurchaseOrderSummary();
            return View(model);
        }
    }
}