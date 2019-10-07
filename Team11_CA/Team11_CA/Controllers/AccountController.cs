using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_CA.Shop.DataAccess.Repositories;
using Team11_CA.Shop.Core.Library;
using Team11_CA.Shop.Core.Models;
using Team11_CA.Shop.Core.ViewModels;

namespace Team11_CA.Controllers
{
    public class AccountController : Controller
    {
        private readonly CustomerRepository context;

        public AccountController()
        {
            this.context = new CustomerRepository();
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Check if customer exists in database
            Customer customer = context.GetValidCustomer(model.Username);
            if (customer == null)
            {
                ModelState.AddModelError("Username", "Username not found");
                return View(model);
            }
            else
            {
                //To verify password provided by client against hashed password in database
                PasswordHash hash = new PasswordHash();
                bool isValidCustomer = hash.VerifyHashedPassword(customer.Password, model.Password);
                if (isValidCustomer)
                {
                    Session["SessionID"] = Guid.NewGuid().ToString();
                    Session["UserID"] = customer.Id;
                    Session["Username"] = customer.Username;
                    Session["FirstName"] = customer.FirstName;
                    Session["LastName"] = customer.LastName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect Password");
                    return View(model);
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel customer)
        {
            PasswordHash hash = new PasswordHash();
            context.Add(new Customer
            {
                //Store hashed password into the database
                Password = hash.HashPassword(customer.Password),
                Username = customer.Username,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Email = customer.Email,
                PostalCode = customer.PostalCode,
                PhoneNumber = customer.PhoneNumber,
                Id = Guid.NewGuid().ToString()
            });

            context.Commit();

            return RedirectToAction("Login", "Account");
        }
    }
}