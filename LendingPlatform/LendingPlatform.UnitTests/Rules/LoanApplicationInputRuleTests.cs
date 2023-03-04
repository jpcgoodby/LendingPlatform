using Xunit;
using LendingPlatform.Service.Rules;
using LendingPlatform.Service.Requests;
using FizzWare.NBuilder.Dates;

namespace LendingPlatform.UnitTests.Rules
{
    public class LoanApplicationInputRuleTests
    {
        private LoanApplicationInputsRules _rule;

        public LoanApplicationInputRuleTests()
        {
            _rule = new LoanApplicationInputsRules();
        }

        [Fact]
        public void Given_Valid_Parsed_Loan_Application_Arguments_Vaidation_Successful()
        {
            var loanApplication = new LoanApplicationRequest(new []{ "2000", "33888", "766" });

            var result = _rule.Validate(loanApplication);

            Assert.NotNull(result);
            Assert.True(result.IsValid);

        }

        [Fact]
        public void Given_Invalid_Parsed_Loan_Application_Arguments_Invalid_CreditScore_Validation_Fail()
        {
            var loanApplication = new LoanApplicationRequest(new[] { "2000", "33888", "1000" });

            var result = _rule.Validate(loanApplication);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal("The credit amount should between 1 - 999.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Invalid_Parsed_Loan_Application_Arguments_Invalid_Amount_Validation_Fail()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "", "33888", "766" }));

            Assert.False(result.IsValid);
            Assert.Equal("The loan amount is invalid.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Invalid_Parsed_Loan_Application_Arguments_Invalid_AssetValue_Validation_Fail()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "2000", "", "766" }));

            Assert.False(result.IsValid);
            Assert.Equal("The assert value is invalid.", result.Errors[0].ErrorMessage);
        }
    }
}



