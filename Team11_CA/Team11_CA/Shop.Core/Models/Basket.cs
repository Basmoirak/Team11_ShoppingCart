using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.Models
{
    public class Basket : BaseEntity
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public string CustomerId { get; set; }

        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
            this.CustomerId = HttpContext.Current.Session["UserID"].ToString();
        }
    }
}