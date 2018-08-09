using CustomerInfoApp.API.Validators.Customer;
using CustomerInfoApp.Contracts.Customer;
using CustomerInfoApp.Mappers.Mappers;
using CustomerInfoApp.Models.Models;
using CustomerInfoApp.Services;
using FluentValidation.Results;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerInfoApp.API.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerContractValidator _customerContractValidator;
        private readonly ICustomerService _customerService;

        public CustomerController()
        {

        }
        public CustomerController(ICustomerContractValidator customerContractValidator, ICustomerService customerService)
        {
            _customerService = customerService;
            _customerContractValidator = customerContractValidator;
        }

        /// <summary>
        /// Returns the Customer based on ID
        /// </summary>
        /// <returns></returns>
        /// <returns>200 - OK, if everything is fine</returns>
        /// <returns>400 - BadRequest, incase of validation issue</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(string[]))]
        public HttpResponseMessage Get()
        {
            var customerInfo = _customerService.Get();
            if (customerInfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Customer data not found");
            }

            var customerDetails = CustomerMapper.Map(customerInfo.ToList());

            return Request.CreateResponse(HttpStatusCode.OK, customerDetails);
        }

        /// <summary>
        /// Returns the Customer based on ID
        /// </summary>
        /// <returns></returns>
        /// <returns>200 - OK, if everything is fine</returns>
        /// <returns>400 - BadRequest, incase of validation issue</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns this status code if the request was successful", Type = typeof(CustomerContract))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Incase of validation issue")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Customer with Id not found")]
        public HttpResponseMessage GetById([FromUri]long id)
        {
            if (id == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is required");
            }
            
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Customer with {id} not found");
            }

            var customerDetails = CustomerMapper.Map(customer);
            return Request.CreateResponse(HttpStatusCode.OK, customerDetails);
        }

        /// <summary>
        /// For creating an customer information entry
        /// </summary>
        /// <param name="contract">contract that will be passed to this method</param>
        /// <returns>200 - OK, if everything is fine</returns>
        /// <returns>400 - BadRequest, incase of model and model state validation</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns this status code if the request was successful")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "In case of invalid model data or if model validation fails")]
        public HttpResponseMessage Post(CustomerContract contract)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
            ValidationResult validationResult = _customerContractValidator.Validate(contract);

            if (!validationResult.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "This request is invalid, please review your request parameters. Errors: " + string.Join(", ",validationResult.Errors));
            }

            Customer customer = CustomerMapper.Map(contract);
            var addedCustomer = _customerService.Add(contract);
            _customerService.Save();
            return Request.CreateResponse(HttpStatusCode.OK, addedCustomer.Id);
        }

        /// <summary>
        /// For updating an customer information entry
        /// </summary>
        /// <param name="contract">contract that will be passed to this method</param>
        /// <returns>200 - OK, if everything is fine</returns>
        /// <returns>400 - BadRequest, incase of model and model state validation</returns>
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns this status code if the request was successful")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "In case of invalid model data or if model validation fails")]
        public HttpResponseMessage Put(CustomerContract contract)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (contract.Id <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please check, Id of the customer is missing");
            }

            ValidationResult validationResult = _customerContractValidator.Validate(contract);

            if (!validationResult.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "This request is invalid, please review your request parameters. Errors: " + string.Join(", ", validationResult.Errors));
            }

            Customer customer = CustomerMapper.Map(contract);
            _customerService.Edit(customer, contract);
            _customerService.Save();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        /// <summary>
        /// For deleting an customer entry
        /// </summary>
        /// <param name="id">id of the customer entry</param>
        /// <returns>200 - OK, if everything is fine</returns>
        /// <returns>400 - BadRequest, incase of model and model state validation</returns>
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns this status code if the request was successful")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "In case of invalid model data or if model validation fails")]
        public HttpResponseMessage Delete([FromUri]long id)
        {
            if (id == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is required");
            }

            var customer = _customerService.GetById(id);
            customer.Status = false;
            _customerService.Edit(customer);
            _customerService.Save();

            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
}
