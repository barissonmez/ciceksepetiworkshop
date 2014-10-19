using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CicekSepeti.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Func<T, Boolean> predicate);
        T GetById(Guid id);
        T Get(Func<T, Boolean> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Func<T, bool> where);
        IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children1, string children2);
        IQueryable<T> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);

    }
}
