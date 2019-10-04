using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Contracts;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //Custom method to return filtered product list based on product name in search string
        public IEnumerable<Product> GetFilteredProductList(string searchStr, IEnumerable<Product> productList)
        {
            IEnumerable<Product> filter = productList.Where(product =>
                product.Name.IndexOf(searchStr, StringComparison.CurrentCultureIgnoreCase) != -1)
                .ToList();

            return filter;
        }
    }
}