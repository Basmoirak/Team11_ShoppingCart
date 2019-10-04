using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.ViewModels
{
    public class BasketSummaryViewModel
    {
        public int BasketCount { get; set; }
        public decimal BasketTotal { get; set; }
        public BasketSummaryViewModel()
        {
        }

        /*So that when we create a new BasketSummaryViewModel, we can default
        the basket count and basket total to zero*/
        public BasketSummaryViewModel(int basketCount, decimal basketTotal)
        {
            this.BasketCount = basketCount;
            this.BasketTotal = basketTotal;
        }
    }
}