using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_CA.Shop.Core.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}