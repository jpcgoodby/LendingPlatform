using FluentValidation;
using LendingPlatform.Domain;
using LendingPlatform.Service.Requests;
using MediatR;
using System.Security.AccessControl;

namespace LendingPlatform.Service.Behaviours
{
    public class RulesBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _rules;
        private readonly ILoanApplicationData<LoanApplication> _loanApplicationData;

        public RulesBehaviour(IEnumerable<IValidator<TRequest>> rules, ILoanApplicationData<LoanApplication> loanApplicationData) => (_rules, _loanApplicationData) = (rules, loanApplicationData);

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_rules.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                foreach(var rule in _rules)
                {
                    var validationResults = await rule.ValidateAsync(context, cancellationToken);
                    if (!validationResults.IsValid)
                    {
                        var errorLoan = request as LoanApplicationRequest;

                        if (validationResults.Errors.Count > 0)
                        {
                            foreach (var validationError in validationResults.Errors)
                            {
                                errorLoan.Error += validationError.ErrorMessage + ", ";
                            }

                            errorLoan.Error = errorLoan.Error.Substring(0, errorLoan.Error.Length - 2);

                            break;
                        }

                    }
                }
            }

            return await next();
        }
    }
}
