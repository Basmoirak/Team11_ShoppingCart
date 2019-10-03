using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_CA.DataAccess.Repositories;

namespace Team11_CA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository context;

        public HomeController()
        {
            this.context = new ProductRepository();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}