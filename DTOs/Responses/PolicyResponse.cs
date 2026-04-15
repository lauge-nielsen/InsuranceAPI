using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;

namespace InsuranceAPI.DTOs.Responses
{
    public class PolicyResponse(Policy policy)
    {
        public int PolicyNumber { get; private set; } = policy.PolicyNumber;
        public Customer Customer { get; set; } = policy.Customer;
        public InsuranceResponse Insurance { get; set; } = new(policy.Insurance);
        public DateTime EffectiveDate { get; set; } = policy.EffectiveDate;
        public DateTime ExpirationDate { get; set; } = policy.ExpirationDate;
        public double Price { get; set; } = policy.Price;
    }
}
