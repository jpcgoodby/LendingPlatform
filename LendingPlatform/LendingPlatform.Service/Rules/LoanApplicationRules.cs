using FluentValidation;
using LendingPlatform.Service.Requests;

namespace LendingPlatform.Service.Rules
{
    public class LoanApplicationRules : AbstractValidator<LoanApplicationRequest>
    {
        public LoanApplicationRules()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(loan => loan.Amount).InclusiveBetween(100000, 1500000).WithMessage("If the value of the loan is more than 1.5 million or less than £100000 then the application is declined.");

            When(loan => loan.Amount >= 1000000, () => {
                RuleFor(loan => loan.Ltv).InclusiveBetween(0, 60).DependentRules(() =>
                {
                    RuleFor(loan => loan.CreditScore).LessThanOrEqualTo(950).WithMessage("The LTV must be 60% or less and the credit score of the applicant must be 950 or more.");
                });
            });

            When(loan => loan.Amount < 1000000, () =>
            {
                When(loan => loan.Ltv < 60, () =>
                {
                    RuleFor(loan => loan.CreditScore).GreaterThan(750).WithMessage("The LTV must be 60% or less and the credit score of the applicant must be 750 or more.");
                });
                When(loan => loan.Ltv >= 60 && loan.Ltv < 80, () =>
                {
                    RuleFor(loan => loan.CreditScore).GreaterThan(800).WithMessage("The LTV must be 80% or less and the credit score of the applicant must be 800 or more.");
                });
                When(loan => loan.Ltv >= 80 && loan.Ltv < 90, () =>
                {
                    RuleFor(loan => loan.CreditScore).GreaterThan(900).WithMessage("The LTV must be 90% or less and the credit score of the applicant must be 900 or more.");
                });
                When(loan => loan.Ltv >= 90 && loan.Ltv <= 100, () =>
                {
                    RuleFor(loan => loan.Ltv).LessThanOrEqualTo(90).WithMessage("The LTV must not be 90% or more.");
                });
            });
        }

    }
}
