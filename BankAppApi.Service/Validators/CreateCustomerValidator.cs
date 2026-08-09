using BankAppApi.Core.DTO.Customer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppApi.Service.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerDTO>
    {
        public CreateCustomerValidator()
        {
            //Rule for customer identity number. İt must be 11 digits and all digits must be numbers.
            RuleFor(c => c.CustomerIdentityNumber)
                .NotEmpty().WithMessage("Identity number is required.")
                .Length(11).WithMessage("Identity number must be 11 digits.")
                .Matches(@"^\d{11}$").WithMessage("Identity number must be all digits.");

            // Rule for customer name. İt must be not empty and max length is 100 characters.
            RuleFor(c => c.CustomerFullName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name must be less than 100 characters.");

            // Rule for customer risk limit . İt must be greater than 0
            RuleFor(c => c.CustomerRiskLimit)
                .GreaterThan(0).WithMessage("Customer risk limit must be greater than 0.");



        }


    }
}

