using LendingPlatform.Service.Models;
using MediatR;

namespace LendingPlatform.Service.Requests
{
    public class LoanApplicationRequest : IRequest<LoanApplicationResponse>
    {
        public LoanApplicationRequest(string[] args)
        {
            Amount = (args[0].Length > 0) ? decimal.Parse(args[0]) : null;
            AssetValue = (args[1].Length > 0) ? decimal.Parse(args[1]) : null;
            CreditScore = (args[2].Length > 0) ? int.Parse(args[2]) : null;

            if (Amount != null && AssetValue != null)
            {
                Ltv = (Amount.Value / AssetValue.Value) * 100;
            }
            
        }
        public decimal? Amount { get; }

        public decimal? AssetValue { get; }

        public int? CreditScore { get; }

        public decimal Ltv { get; }

        public string? Error { get; set; }
    }
}
