using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_CA.Shop.Core.ViewModels;
using Team11_CA.Shop.Services;


namespace Team11_CA.Controllers
{
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
            List<BasketItemViewModel> model = basketService.GetBasketItems(this.HttpContext);

            return View(model);
        }

        public ActionResult AddToBasket(string Id, string quantity)
        {
            basketService.AddToBasket(this.HttpContext, Id, quantity);

            return RedirectToAction("Index","Home");
        }
        public ActionResult UpdateBasket(string Id, string quantity)
        {
            basketService.UpdateBasket(this.HttpContext, Id, quantity);

            return RedirectToAction("Index", "Basket");
        }

        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index");
        }
        public ActionResult ClearBasket()
        {
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            BasketSummaryViewModel basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }
  
    }
}