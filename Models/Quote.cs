using InsuranceAPI.Services;

namespace InsuranceAPI.Models
{
    public class Quote
    {
        public string QuoteId { get; set; } = Guid.NewGuid().ToString();
        public Customer Customer { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string InsuranceType { get; set; } = null!;
        public double Price { get; set; }
        public DateOnly EffectiveDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Quote(string customerId, string insuranceType, DateOnly effectiveDate)
        {
            ValidateQuoteParameters(customerId, insuranceType, effectiveDate);

            Customer = InsuranceService.GetCustomerById(customerId);
            InsuranceType = insuranceType;
            Price = InsuranceService.CalculatePrice(Customer, InsuranceType);
            EffectiveDate = effectiveDate;
            ExpirationDate = effectiveDate.AddYears(1);
        }

        private static void ValidateQuoteParameters(string customerId, string insuranceType, DateOnly effectiveDate)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("CustomerId required");
            if (string.IsNullOrWhiteSpace(insuranceType))
                throw new ArgumentException("InsuranceType required");
            if (effectiveDate < DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentException("EffectiveDate cannot be in the past");
        }

        public void ValidateQuote()
        {
            if (Customer == null)
                throw new ArgumentException("Customer required");
            if (string.IsNullOrWhiteSpace(InsuranceType))
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
