using LendingPlatform.Domain;
using LendingPlatform.Service.Handlers;
using LendingPlatform.Service.Requests;
using Moq;
using Xunit;

namespace LendingPlatform.UnitTests.Handlers
{
    public class LendingPlatformServiceTests
    {
        private readonly LoanApplicationHandler _handler;
        private readonly Mock<ILoanApplicationData<LoanApplication>> _data;
        private readonly LoanApplicationRequest _request;
        private readonly LoanApplication _loanApplication;

        public LendingPlatformServiceTests()
        {
            _data = new Mock<ILoanApplicationData<LoanApplication>>();
            _request = new LoanApplicationRequest(new[] { "2000", "33888", "766" });
            _handler = new LoanApplicationHandler(_data.Object);
            _loanApplication = new LoanApplication { 
                Amount = 2000,
                AssetValue = 33888,
                CreditScore = 766
            };
        }

        [Fact]
        public void Given_Valid_Loan_Application_Submit_Loan_Application()
        {
            var totalApplicationResults = new List<LoanApplication>
            {
                _loanApplication,
            };

            _data.Setup(x => x.Submit(_loanApplication));
            _data.Setup(x => x.Get()).Returns(totalApplicationResults);

            var metrics = _handler.Handle(new LoanApplicationRequest(new[] { "2000", "33888", "766" }), new CancellationToken()).Result;

            Assert.NotNull(metrics);
            Assert.False(metrics.Success);
            Assert.Single(metrics.TotalLoanApplications);
            Assert.Equal("£2,000", metrics.TotalLoansValue);
            Assert.Equal("£2,000", metrics.AverageLoanToValue);
        }
    }
}



