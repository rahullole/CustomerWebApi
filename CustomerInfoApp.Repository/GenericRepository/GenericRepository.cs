using CustomerInfoApp.Models.AuditEntries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerInfoApp.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }
        public T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        public IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public IEnumerable<T> GetByNavigationProperties(Expression<Func<T, bool>> predicate, params string[] children)
        {
            var query = _dbset.Where(predicate);

            foreach (var child in children)
            {
                query = query.Include(child);
            }

            return query.AsEnumerable();
        }

        public void Save()
        {
            _entities.SaveChanges();
        }

        public T GetById(object id)
        {
            return _dbset.Find(id);
        }
    }
}
