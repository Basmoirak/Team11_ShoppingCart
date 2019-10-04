﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.Models
{
    public class OrderItem
    {
        public string ProductID { get; set; }
        public string CustomerID { get; set; }
        public DateTime DateCreated { get; set; }
        public string ActivationCode { get; set; }

        public OrderItem()
        {
            this.DateCreated = DateTime.Now;
            this.ActivationCode = Guid.NewGuid().ToString();
        }
    }
}