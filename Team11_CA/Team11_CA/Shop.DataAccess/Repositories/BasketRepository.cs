using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Contracts;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.Shop.DataAccess.Repositories
{
    public class BasketRepository : Repository<Basket>, IRepository<Basket>
    {
        public Basket GetBasketFromUserID(string Id)
        {
            return _context.Set<Basket>()
                .Where(x => x.CustomerId == Id)
                .FirstOrDefault();
        }
    }
}