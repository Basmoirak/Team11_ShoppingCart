using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Contracts;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.Shop.DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        //Custom method to retrieve customer object based on username
        public Customer GetValidCustomer(string username)
        {
            return _context.Set<Customer>()
                .Where(x => x.Username == username)
                .FirstOrDefault();
        }
    }
}