using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_CA.Shop.DataAccess.Repositories;
using Team11_CA.Shop.Core.Models;
using Team11_CA.Shop.Core.Filters;

namespace Team11_CA.Controllers
{
    [LoginFilter]
    public class HomeController : Controller
    {
        private readonly ProductRepository context;

        public HomeController()
        {
            this.context = new ProductRepository();
        }

        public ActionResult Index(string searchStr = null)
        {
            //Retrieve all products from database
            IEnumerable<Product> productList = context.GetAll().ToList();

            if(searchStr == null)
            {
                return View(productList);
            }
            else
            {
                //Filter and return list of products based on search string
                productList = context.GetFilteredProductList(searchStr, productList);
                TempData["searchStr"] = searchStr;
                return View(productList);
            }
        }

        public ActionResult ProductDetails(string Id)
        {
            //Retrieve product from database based on productId
            Product product = context.Get(Id);

            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
    }
}