using CustomerInfoApp.Models.AuditEntries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CustomerInfoApp.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate);
        T GetById(object id);
        IEnumerable<T> GetByNavigationProperties(Expression<Func<T, bool>> predicate, params string[] children);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
