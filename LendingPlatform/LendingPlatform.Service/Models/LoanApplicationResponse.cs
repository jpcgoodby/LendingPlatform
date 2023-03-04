using LendingPlatform.Domain;
using System.Globalization;

namespace LendingPlatform.Service.Models
{
    public class LoanApplicationResponse
    {
        public LoanApplicationResponse(List<LoanApplication> totalApplicantLoanStatus)
        {
            TotalLoanApplications = totalApplicantLoanStatus;

            decimal totalLoansValue = 0;

            foreach (var applicantLoan in totalApplicantLoanStatus)
            {
                totalLoansValue += applicantLoan.Amount;
            }

            TotalLoansValue = string.Format(new CultureInfo("en-GB", false), "{0:c0}", totalLoansValue);
            AverageLoanToValue = string.Format(new CultureInfo("en-GB", false), "{0:c0}", totalLoansValue / TotalLoanApplications.Count);
            Success = TotalLoanApplications.LastOrDefault().Success;
            ErrorMessage = TotalLoanApplications.LastOrDefault().Error;
        }

        public bool Success { get; }

        public List<LoanApplication> TotalLoanApplications { get; }

        public string TotalLoansValue { get; }

        public string AverageLoanToValue { get; }

        public string ErrorMessage { get; }
    }
}
