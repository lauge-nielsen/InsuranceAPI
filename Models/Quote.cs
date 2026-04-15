using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.Domain.Validation;
using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models.Insurances;
using InsuranceAPI.Services.Factories;

namespace InsuranceAPI.Models
{
    public class Quote
    {
        public string QuoteId { get; set; } = Guid.NewGuid().ToString();
        public Customer Customer { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public double Price { get; set; }
        public Insurance Insurance { get; set; }
        public QuoteStatus QuoteStatus { get; set; } = QuoteStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Quote(Customer customer, InsuranceRequest insuranceRequest, DateOnly effectiveDate)
        {
            Customer = customer;
            EffectiveDate = effectiveDate;
            ExpirationDate = effectiveDate.AddYears(1);
            Insurance = InsuranceFactory.Create(insuranceRequest);

            Validate();
        }

        public Quote()
        {

        }

        public void Validate()
        {
            QuoteValidation.ValidateStructure(this);
            QuoteBusinessRules.EnforceBusinessRules(this);
        }

    }
}
