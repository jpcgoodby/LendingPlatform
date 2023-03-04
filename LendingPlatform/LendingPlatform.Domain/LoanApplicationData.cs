namespace LendingPlatform.Domain
{
    public class LoanApplicationData : ILoanApplicationData<LoanApplication>
    {
        private readonly List<LoanApplication> _loanApplication;

        public LoanApplicationData()
        {
            _loanApplication = MockApplicationData.LoadData();
        }

        public List<LoanApplication> Get()
        {
            return _loanApplication;
        }

        public void Submit(LoanApplication applicationResult)
        {
            _loanApplication.Add(applicationResult);
        }
    }
}
