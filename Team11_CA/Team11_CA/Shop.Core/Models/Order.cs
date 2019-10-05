using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.CustomerID = HttpContext.Current.Session["UserID"].ToString();
            this.OrderItems = new List<OrderItem>();
        }

        public string CustomerID { get; set; }
        public string OrderStatus { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}