using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Contracts;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.Shop.DataAccess.Repositories
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

        public string GetProductImage(string productID, IEnumerable<Product> productList)
        {
            string image = productList.Where(product => product.Id == productID)
                                      .Select(x => x.Image).FirstOrDefault();
            return image;
        }

        public string GetProductDescription(string productID, IEnumerable<Product> productList)
        {
            string description = productList.Where(product => product.Id == productID)
                                            .Select(x => x.Description).FirstOrDefault();
            return description;
        }

        public string GetProductName(string productID, IEnumerable<Product> productList)
        {
            string name = productList.Where(product => product.Id == productID)
                                            .Select(x => x.Name).FirstOrDefault();
            return name;
        }
    }
}