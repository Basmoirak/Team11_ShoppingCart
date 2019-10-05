using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.Shop.Core.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetFilteredProductList(string searchStr, IEnumerable<Product> products);
        string GetProductImage(string productID, IEnumerable<Product> productList);
        string GetProductDescription(string productID, IEnumerable<Product> productList);
        string GetProductName(string productID, IEnumerable<Product> productList);
    }
}