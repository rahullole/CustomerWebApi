using CustomerInfoApp.Models.Models;
using CustomerInfoApp.Repositories.GenericRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerInfoApp.API.Tests
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Mock Customer Repository for use in testing
        /// </summary>
        public readonly IGenericRepository<Customer> MockCustomersRepository;

        public UnitTest()
        {
            // create some mock Customers to play with
            IList<Customer> Customers = new List<Customer>
                {
                    new Customer { Id = 1, FirstName = "A", LastName = "A", Email = "a@a.com", PhoneNumber = "1234567890", Status = true },
                    new Customer { Id = 2, FirstName = "B", LastName = "B", Email = "b@b.com", PhoneNumber = "1234567890", Status = true }
                };

            // Mock the Customer Repository using Moq
            Mock<IGenericRepository<Customer>> mockCustomerRepository = new Mock<IGenericRepository<Customer>>();            

            // Return all the Customer
            mockCustomerRepository.Setup(mr => mr.GetAll()).Returns(Customers);

            // return a Customer by Id
            mockCustomerRepository.Setup(mr => mr.GetById(
                It.IsAny<int>())).Returns((int i) => Customers.Where(
                x => x.Id == i).Single());

            // Allows us to test saving a Customer
            mockCustomerRepository.Setup(mr => mr.Add(It.IsAny<Customer>())).Returns(
                (Customer target) =>
                {
                    DateTime now = DateTime.Now;
                    target.CreatedDate = now;
                    target.UpdatedDate = now;
                    target.Id = Customers.Count() + 1;
                    Customers.Add(target);                   

                    return target;
                });

            // Allows us to test updating a Customer
            mockCustomerRepository.Setup(mr => mr.Edit(It.IsAny<Customer>())).Callback(
                (Customer target) =>
                {
                    DateTime now = DateTime.Now;
                    var exists = Customers.Where(
                            q => q.Id == target.Id).Single();

                    if (exists != null)
                    {
                        exists.FirstName = target.FirstName;
                        exists.LastName = target.LastName;
                        exists.PhoneNumber = target.PhoneNumber;
                        exists.Email = target.Email;
                        exists.UpdatedDate = now;
                    }
                });

            // Complete the setup of our Mock Customer Repository
            this.MockCustomersRepository = mockCustomerRepository.Object;
        }

        /// <summary>
        /// return a Customer By Id
        /// </summary>
        [TestMethod]
        public void CanReturnCustomerById()
        {
            // Try finding a Customer by id
            Customer testCustomer = this.MockCustomersRepository.GetById(1);

            Assert.IsNotNull(testCustomer); // Test if null
            Assert.IsInstanceOfType(testCustomer, typeof(Customer)); // Test type
            Assert.AreEqual("A", testCustomer.FirstName); // Verify it is the right Customer
        }

        /// <summary>
        /// return all Customers
        /// </summary>
        [TestMethod]
        public void CanReturnAllCustomers()
        {
            // Try finding all Customers
            IList<Customer> testCustomers = this.MockCustomersRepository.GetAll().ToList();

            Assert.IsNotNull(testCustomers); // Test if null
            Assert.AreEqual(2, testCustomers.Count); // Verify the correct count
        }


        /// <summary>
        /// Add a new Customer
        /// </summary>
        [TestMethod]
        public void CanInsertCustomer()
        {
            // Create a new Customer, not I do not supply an id
            Customer newCustomer = new Customer { Id = 3, FirstName = "C", LastName = "C", Email = "c@c.com", PhoneNumber = "1234567890", Status = true };

            int CustomerCount = this.MockCustomersRepository.GetAll().ToList().Count;
            Assert.AreEqual(2, CustomerCount); // Verify the expected Number pre-insert

            // try saving our new Customer
            this.MockCustomersRepository.Add(newCustomer);

            // demand a recount
            CustomerCount = this.MockCustomersRepository.GetAll().ToList().Count;
            Assert.AreEqual(3, CustomerCount); // Verify the expected Number post-insert

            // verify that our new Customer has been saved
            Customer testCustomer = this.MockCustomersRepository.GetAll().FirstOrDefault(x => x.FirstName == "C");
            Assert.IsNotNull(testCustomer); // Test if null
            Assert.IsInstanceOfType(testCustomer, typeof(Customer)); // Test type
            Assert.AreEqual(3, testCustomer.Id); // Verify it has the expected Customer id
        }

        /// <summary>
        /// update a Customer
        /// </summary>
        [TestMethod]
        public void CanUpdateCustomer()
        {
            // Find a Customer by id
            Customer testCustomer = this.MockCustomersRepository.GetById(1);

            // Change one of its properties
            testCustomer.FirstName = "AAA";

            // Save our changes.
            this.MockCustomersRepository.Edit(testCustomer);

            // Verify the change
            Assert.AreEqual("AAA", this.MockCustomersRepository.GetById(1).FirstName);
        }

    }
}
