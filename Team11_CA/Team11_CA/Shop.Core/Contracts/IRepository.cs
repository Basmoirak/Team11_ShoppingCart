using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Team11_CA.Shop.Core.Models;

namespace Team11_CA.Shop.Core.Contracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(string Id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Commit();
    }
}