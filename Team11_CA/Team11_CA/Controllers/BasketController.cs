using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_CA.Shop.Core.Filters;
using Team11_CA.Shop.Core.ViewModels;
using Team11_CA.Shop.Services;

namespace Team11_CA.Controllers
{
    [LoginFilter]
    public class BasketController : Controller
    {
        BasketService basketService;

        public BasketController()
        {
            this.basketService = new BasketService();
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
  
    }
}