using CustomerInfoApp.Contracts.Customer;
using CustomerInfoApp.Models.Models;
using System.Collections.Generic;

namespace CustomerInfoApp.Mappers.Mappers
{
    public static class CustomerMapper
    {
        public static Customer Map(CustomerContract customerContract)
        {
            return new Customer
            {
                FirstName = customerContract.FirstName,
                LastName = customerContract.LastName,
                Email = customerContract.Email,
                PhoneNumber = customerContract.PhoneNumber,
                Status = customerContract.Status
            };

        }

        public static Customer Map(Customer customer, CustomerContract customerContract)
        {
            customer.FirstName = customerContract.FirstName;
            customer.LastName = customerContract.LastName;
            customer.Email = customerContract.Email;
            customer.PhoneNumber = customerContract.PhoneNumber;

            return customer;
        }
        public static CustomerContract Map(Customer customer)
        {
            return new CustomerContract
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Status = customer.Status
            };

        }

        public static List<CustomerContract> Map(List<Customer> customers)
        {
            List<CustomerContract> customerList = new List<CustomerContract>();

            foreach (Customer customer in customers)
            {
                customerList.Add(Map(customer));
            }

            return customerList;
        }

    }
}
