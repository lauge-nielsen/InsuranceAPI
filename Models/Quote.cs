using InsuranceAPI.Services;

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
        public QuoteStatus QuoteStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Quote(Customer customer, InsuranceType insuranceType, DateOnly effectiveDate, double price)
        {
            Customer = customer;
            InsuranceType = insuranceType;
            EffectiveDate = effectiveDate;
            ExpirationDate = effectiveDate.AddYears(1);
            Price = price;
            QuoteStatus = QuoteStatus.Quoted;
            
            Validate();
        }

        public Quote()
        {

        }

        public void Validate()
        {
            ValidateData();
            ValidateBusinessRules();
        }

        public void ValidateData()
        {
            if (string.IsNullOrWhiteSpace(QuoteId))
                throw new ArgumentException("QuoteId required");

            if (Customer == null)
                throw new ArgumentException("Customer required");

            if (EffectiveDate < DateOnly.FromDateTime(DateTime.UtcNow.Date))
                throw new ArgumentException("EffectiveDate cannot be in the past");

            if (ExpirationDate <= EffectiveDate)
                throw new ArgumentException("ExpirationDate must be after EffectiveDate");
            
        }

        public void ValidateBusinessRules()
        {
            if (Customer.Age() < 20 && InsuranceType == InsuranceType.Spacecraft)
                throw new InvalidOperationException("Customer must be at least 20 years old to purchase a Spacecraft insurance");
        }
    }
}
