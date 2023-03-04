using LendingPlatform.Service.Rules;
using LendingPlatform.Service.Requests;
using Xunit;

namespace LendingPlatform.UnitTests.Rules
{
    public class LoanApplicationRuleTests
    {
        private readonly LoanApplicationRules _rule;
        public LoanApplicationRuleTests() => _rule = new LoanApplicationRules();

        [Fact]
        public void Given_Loan_Application_Is_Above_1_And_Half_Million()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "1500001", "33888", "766" }));

            Assert.False(result.IsValid);
            Assert.Equal("If the value of the loan is more than 1.5 million or less than £100000 then the application is declined.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_100000_Million()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "90001", "33888", "766" }));

            Assert.False(result.IsValid);
            Assert.Equal("If the value of the loan is more than 1.5 million or less than £100000 then the application is declined.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Loan_Application_Is_Above_1_Million_And_LtV_Less_60_Percent_Above_950_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "1000001", "3000000", "951" }));

            Assert.False(result.IsValid);
            Assert.Equal("The LTV must be 60% or less and the credit score of the applicant must be 950 or more.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Loan_Application_Is_Above_1_Million_And_LtV_Less_60_Percent_Below_950_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "1000001", "3000000", "949" }));

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_1_Million_And_LtV_Less_60_Percent_Below_750_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "999999", "2000000", "749" }));

            Assert.False(result.IsValid);
            Assert.Equal("The LTV must be 60% or less and the credit score of the applicant must be 750 or more.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_1_Million_And_LtV_Less_60_Percent_Above_750_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "999999", "2000000", "751" }));

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_1_Million_And_LtV_Less_80_Percent_Below_800_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "999999", "1300000", "799" }));

            Assert.False(result.IsValid);
            Assert.Equal("The LTV must be 80% or less and the credit score of the applicant must be 800 or more.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_1_Million_And_LtV_Less_80_Percent_Above_800_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "999999", "1300000", "801" }));

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_1_Million_And_LtV_Less_90_Percent_Below_900_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "999999", "1200000", "899" }));

            Assert.False(result.IsValid);
            Assert.Equal("The LTV must be 90% or less and the credit score of the applicant must be 900 or more.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_1_Million_And_LtV_Less_90_Percent_Above_900_Credit_Score()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "999999", "1200000", "901" }));

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Loan_Application_Is_Below_1_Million_And_LtV_Above_90_Percent()
        {
            var result = _rule.Validate(new LoanApplicationRequest(new[] { "999999", "1100000", "899" }));

            Assert.False(result.IsValid);
            Assert.Equal("The LTV must not be 90% or more.", result.Errors[0].ErrorMessage);
        }
    }
}



