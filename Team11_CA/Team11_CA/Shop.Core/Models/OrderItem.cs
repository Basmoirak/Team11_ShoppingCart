using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public DateTime DateCreated { get; set; }
        public string ActivationCode { get; set; }

        public OrderItem()
        {
            this.DateCreated = DateTime.Now;
            this.ActivationCode = Guid.NewGuid().ToString();
        }
    }
}