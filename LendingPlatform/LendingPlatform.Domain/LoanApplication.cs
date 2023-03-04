namespace LendingPlatform.Domain
{
    public class LoanApplication
    {
        public decimal Amount { get; set; }

        public decimal AssetValue { get; set; }

        public int CreditScore { get; set; }

        public bool Success { get; set; }

        public string? Error { get; set; }
    }
}
