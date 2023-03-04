namespace LendingPlatform.Domain
{
    public interface ILoanApplicationData<TRequest>
    {
        void Submit(TRequest applicationResult);

        List<TRequest> Get();

    }
}
