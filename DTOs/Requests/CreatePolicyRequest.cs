using InsuranceAPI.Models;

namespace InsuranceAPI.DTOs.Requests
{
    public class CreatePolicyRequest(string customerId, string insuranceType)
    {
        public string CustomerId { get; set; } = customerId;
        public string InsuranceType { get; set; } = insuranceType;
    }
}
