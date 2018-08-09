using CustomerInfoApp.Contracts.Customer;
using CustomerInfoApp.Models.Models;
using System;
using System.Collections.Generic;

namespace CustomerInfoApp.Services
{
    public interface ICustomerService
    {
        Customer GetById(long id);
        ICollection<Customer> Get();
        IEnumerable<Customer> GetBy(System.Linq.Expressions.Expression<Func<Customer, bool>> predicate);
        IEnumerable<Customer> GetByNavigationProperties(System.Linq.Expressions.Expression<Func<Customer, bool>> predicate, params string[] navigationProperties);
        Customer Add(CustomerContract contract);
        void Edit(Customer customer, CustomerContract contract);
        void Edit(Customer customer);
        void Save();
    }
}
