using InsuranceAPI.Models;
using InsuranceAPI.Services;

namespace InsuranceAPI.DTOs.Requests
{
    public class UpdateQuoteRequest
    {
        public string? CustomerId { get; set; }
        public InsuranceType? InsuranceType { get; set; }
        public DateOnly? EffectiveDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public double? Price { get; set; }
    }
}
