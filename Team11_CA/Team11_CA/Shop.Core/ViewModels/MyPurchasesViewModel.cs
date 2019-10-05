using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.ViewModels
{
    public class MyPurchasesViewModel
    {
        [Display(Name = "Image")]
        public string ProductImage { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Order Created Date")]
        public string OrderCreatedDate { get; set; }

        [Display(Name = "Quantity")]
        public int OrderQuantity { get; set; }

        [Display(Name = "Activation Codes")]
        public IEnumerable<string> ActivationCodes { get; set; }
    }
}