using LendingPlatform.Domain;
using LendingPlatform.Service.Models;
using Xunit;

namespace LendingPlatform.UnitTests.Models
{
    public class LoanApplicationMetricsTests
    {
        [Fact]
        public void Given_Format_Unsuccessful_Loan_Application_Result()
        {
            var totalLoanApplicationResult = new List<LoanApplication>
            {
                new LoanApplication
                {
                    Amount = 2000,
                    AssetValue = 33888,
                    CreditScore = 766,
                    Success = false
                },
               new LoanApplication
                {
                    Amount = 1000,
                    AssetValue = 13888,
                    CreditScore = 766,
                    Success = false
                }
            };

            var model = new LoanApplicationResponse(totalLoanApplicationResult);

            Assert.NotNull(model);
            Assert.Equal("£3,000", model.TotalLoansValue);
            Assert.Equal("£1,500", model.AverageLoanToValue);
            Assert.False(model.Success);
            Assert.Equal(2, model.TotalLoanApplications.Count(x => x.Success == false));
            Assert.Equal(0, model.TotalLoanApplications.Count(x => x.Success == true));
        }

        [Fact]
        public void Given_Format_Successful_Loan_Application_Result()
        {
            var totalLoanApplicationResult = new List<LoanApplication>
            {
                new LoanApplication
                {
                    Amount = 200000,
                    AssetValue = 3000000,
                    CreditScore = 766,
                    Success = true
                },
               new LoanApplication
                {
                    Amount = 200000,
                    AssetValue = 3000000,
                    CreditScore = 666,
                    Success = true
                }
            };

            var model = new LoanApplicationResponse(totalLoanApplicationResult);

            Assert.NotNull(model);
            Assert.Equal("£400,000", model.TotalLoansValue);
            Assert.Equal("£200,000", model.AverageLoanToValue);
            Assert.True(model.Success);
            Assert.Equal(0, model.TotalLoanApplications.Count(x => x.Success == false));
            Assert.Equal(2, model.TotalLoanApplications.Count(x => x.Success == true));
        }
    }
}



