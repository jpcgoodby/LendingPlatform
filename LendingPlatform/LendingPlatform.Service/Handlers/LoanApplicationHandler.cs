using LendingPlatform.Service.Models;
using LendingPlatform.Domain;
using MediatR;
using LendingPlatform.Service.Requests;

namespace LendingPlatform.Service.Handlers
{
    public class LoanApplicationHandler : IRequestHandler<LoanApplicationRequest, LoanApplicationResponse>
    {
        private readonly ILoanApplicationData<LoanApplication> _loanApplicationData;

        public LoanApplicationHandler(ILoanApplicationData<LoanApplication> loanApplicationData) => (_loanApplicationData) = (loanApplicationData);

        public Task<LoanApplicationResponse> Handle(LoanApplicationRequest request, CancellationToken cancellationToken)
        {
            _loanApplicationData.Submit(new LoanApplication
            {
                Amount = request.Amount.Value,
                AssetValue = request.AssetValue.Value,
                CreditScore = request.CreditScore.Value,
                Error = request.Error,
                Success = request.Error == null ? true: false
            });

            var totalApplicants = _loanApplicationData.Get();

            var result = new LoanApplicationResponse(totalApplicants);

            return Task.FromResult(result);
        }
    }
}
