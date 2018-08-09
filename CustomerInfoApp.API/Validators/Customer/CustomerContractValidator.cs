using CustomerInfoApp.Contracts.Customer;
using FluentValidation;

namespace CustomerInfoApp.API.Validators.Customer
{
    public class CustomerContractValidator :AbstractValidator<CustomerContract>, ICustomerContractValidator
    {
        public CustomerContractValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Customer first name is required");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Customer last name is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("Customer phone number is required");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Customer first name is required");
        }
    }
}
