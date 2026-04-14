using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.Domain.Validation;

namespace InsuranceAPI.Models
{
    public class Quote
    {
        public string QuoteId { get; set; } = Guid.NewGuid().ToString();
        public Customer Customer { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public double Price { get; set; }
        public QuoteStatus QuoteStatus { get; set; } = QuoteStatus.Draft;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Quote(Customer customer, InsuranceType insuranceType, DateOnly effectiveDate, double price)
        {
            Customer = customer;
            InsuranceType = insuranceType;
            EffectiveDate = effectiveDate;
            ExpirationDate = effectiveDate.AddYears(1);
            Price = price;
            
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
