using CustomerInfoApp.Contracts.Customer;
using FluentValidation;

namespace CustomerInfoApp.API.Validators.Customer
{
    public class CustomerContractValidator :AbstractValidator<CustomerContract>, ICustomerContractValidator
    {
        public CustomerContractValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Customer first name is required").MaximumLength(20).WithMessage("First name too long");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Customer last name is required").MaximumLength(20).WithMessage("Last name too long");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().WithMessage("Customer phone number is required").MinimumLength(10).WithMessage("A valid phone number is required");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Customer first name is required").EmailAddress().WithMessage("A valid email is required"); ;
        }
    }
}
