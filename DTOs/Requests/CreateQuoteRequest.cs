using Microsoft.Extensions.Diagnostics.Metrics;

namespace InsuranceAPI.DTOs.Requests
{
    public class CreateQuoteRequest
    {
        public required string CustomerId { get; set; } 
        public required string InsuranceType { get; set; } 
        public required DateOnly EffectiveDate { get; set; }
    }
}
