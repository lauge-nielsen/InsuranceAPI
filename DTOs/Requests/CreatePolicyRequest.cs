using InsuranceAPI.Models;

namespace InsuranceAPI.DTOs.Requests
{
    public class CreatePolicyRequest
    {
        public required string QuoteId { get; set; }
    }
}
