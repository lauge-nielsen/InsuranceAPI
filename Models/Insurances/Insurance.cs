using InsuranceAPI.DTOs.Requests;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InsuranceAPI.Models.Insurances
{
    public class Insurance(InsuranceRequest request)
    {
        public InsuranceType InsuranceType { get; set; } = request.InsuranceType;
        public virtual double BasePrice { get; set; } = 500;

        public virtual void Validate(Customer customer)
        {

        }

        public virtual double ApplyPricing(Customer customer, double basePrice)
        {
            //Overridden in subclasses
            return basePrice;
        }
    }
}
