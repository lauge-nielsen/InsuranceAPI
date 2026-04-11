using InsuranceAPI.Services;

namespace InsuranceAPI.Models
{
    public class Quote
    {
        public string QuoteId { get; set; } = Guid.NewGuid().ToString();
        public Customer Customer { get; set; } = null!;
        public InsuranceType InsuranceType { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public double Price { get; set; }
        public QuoteStatus QuoteStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Quote(string customerId, InsuranceType insuranceType, DateOnly effectiveDate)
        {
            ValidateQuoteParameters(customerId, insuranceType, effectiveDate);

            Customer = InsuranceService.GetCustomerById(customerId);
            InsuranceType = insuranceType;
            Price = InsuranceService.CalculatePrice(Customer, InsuranceType);
            EffectiveDate = effectiveDate;
            ExpirationDate = effectiveDate.AddYears(1);
            QuoteStatus = QuoteStatus.Quoted;
        }

        private static void ValidateQuoteParameters(string customerId, InsuranceType insuranceType, DateOnly effectiveDate)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("CustomerId required");
            if (!Enum.IsDefined(typeof(InsuranceType), insuranceType))
                throw new ArgumentException("Invalid InsuranceType");
            if (effectiveDate < DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentException("EffectiveDate cannot be in the past");
        }

        public void ValidateQuote()
        {
            if (Customer == null)
                throw new ArgumentException("Customer required");
            if (!Enum.IsDefined(typeof(InsuranceType), InsuranceType))
                throw new ArgumentException("InsuranceType required");
            if (Price <= 0)
                throw new ArgumentException("Price must be positive");
            if (EffectiveDate < DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentException("EffectiveDate cannot be in the past");
            if (ExpirationDate <= EffectiveDate)
                throw new ArgumentException("ExpirationDate must be after EffectiveDate");
        }
    }
}
