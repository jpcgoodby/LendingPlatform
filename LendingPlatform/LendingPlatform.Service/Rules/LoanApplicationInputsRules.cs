using FluentValidation;
using LendingPlatform.Service.Requests;

namespace LendingPlatform.Service.Rules
{
    public class LoanApplicationInputsRules : AbstractValidator<LoanApplicationRequest>
    {
        public LoanApplicationInputsRules()
        {
            RuleFor(x => x.Amount).NotNull().WithMessage("The loan amount is invalid.");
            RuleFor(x => x.AssetValue).NotNull().WithMessage("The assert value is invalid.");
            RuleFor(x => x.CreditScore).NotNull().InclusiveBetween(1, 999).WithMessage("The credit amount should between 1 - 999.");
        }
    }
}
