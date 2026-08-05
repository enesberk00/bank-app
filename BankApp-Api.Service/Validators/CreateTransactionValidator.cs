using BankApp_Api.Core.DTO.Transaction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp_Api.Service.Validators
{
    internal class CreateTransactionValidator : AbstractValidator<CreateTransactionDTO>
    {
        public CreateTransactionValidator()
        {
            //Rule for card number.legit card number is required
            RuleFor(x => x.CardId)
                .GreaterThan(0).WithMessage("Legit card number is required.");

            //Rule for amount. Amount must be greater than 0
            RuleFor(x => x.TransactionAmount)
                .GreaterThan(0).WithMessage("Transaction Amount must be greater than 0.");

        }
    }
}
