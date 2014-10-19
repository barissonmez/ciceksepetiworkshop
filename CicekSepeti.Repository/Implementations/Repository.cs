using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CicekSepeti.Data;
using CicekSepeti.Data.Contracts;
using CicekSepeti.Repository.Contracts;

namespace CicekSepeti.Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CicekSepetiDbContext _dbContext;
        private readonly IDbSet<T> _dbset;

        public Repository(IDataContextFactory dbContext)
        {
            _dbContext = _dbContext ?? dbContext.Get();
            _dbset = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public void Delete(Func<T, bool> predicate)
        {
            IEnumerable<T> objects = _dbset.Where<T>(predicate).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }

        public T GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        public T Get(Func<T, bool> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        public IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return _dbset.Where(where).ToList();
        }

        public IEnumerable<T> QueryObjectGraph(Expression<Func<T, bool>> filter, string children1, string children2)
        {
            return _dbset.Include(children1).Include(children2).Where(filter);
        }

        /// <summary>
        /// Gets related children objects
        /// Usage: var test = _rentalRepo.GetAllLazyLoad(a => a.State != RentalState.Returned, b => b.Book, b=> b.RentedBy);
        /// </summary>
        /// <param name="filter">Filter By</param>
        /// <param name="children">Related objects to load</param>
        /// <returns>Filtered object with related children</returns>
        public IQueryable<T> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children)
        {
            children.ToList().ForEach(x => _dbset.Include(x).Load());
            return _dbset;
        }


    }
}