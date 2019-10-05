using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.ViewModels
{
    public class MyPurchasesViewModel
    {
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public string OrderCreatedDate { get; set; }
        public string OrderQuantity { get; set; }
        public List<string> ActivationCode { get; set; }
    }
}