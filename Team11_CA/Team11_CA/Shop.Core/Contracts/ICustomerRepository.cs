using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.Shop.Core.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetValidCustomer(string username);
    }
}