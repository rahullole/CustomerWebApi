using CustomerInfoApp.Contracts.Customer;
using CustomerInfoApp.Mappers.Mappers;
using CustomerInfoApp.Models.Models;
using CustomerInfoApp.Repositories.GenericRepository;
using CustomerInfoApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerInfoApp.Service
{
    public class CustomerService: ICustomerService
    {

        private readonly IGenericRepository<Customer> _customerRepository;

        public CustomerService(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer Add(CustomerContract contract)
        {
            Customer customer = _customerRepository.Add(CustomerMapper.Map(contract));            
            return customer;
        }

        public void Edit(Customer customer, CustomerContract contract)
        {
            customer = CustomerMapper.Map(GetById(contract.Id), contract);
            _customerRepository.Edit(customer);
        }

        public void Edit(Customer customer)
        {
            _customerRepository.Edit(customer);
        }

        public ICollection<Customer> Get()
        {
            return _customerRepository.GetAll().ToList(); 
        }

        public IEnumerable<Customer> GetBy(Expression<Func<Customer, bool>> predicate)
        {
            return _customerRepository.GetBy(predicate);
        }

        public Customer GetById(long id)
        {
            return _customerRepository.GetBy(x => x.Id == id && x.Status).FirstOrDefault();
        }

        public IEnumerable<Customer> GetByNavigationProperties(Expression<Func<Customer, bool>> predicate, params string[] navigationProperties)
        {
            return _customerRepository.GetByNavigationProperties(predicate, navigationProperties);
        }

        public void Save()
        {
            _customerRepository.Save();
        }
    }
}
