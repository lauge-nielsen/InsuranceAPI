using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.Domain.Validation;
using InsuranceAPI.Models.Insurances;

namespace InsuranceAPI.Models
{
    public class Policy
    {
        private static int nextPolicyNumber = 1;

        public int PolicyNumber { get; private set; }
        public Customer Customer { get; set; }
        public Insurance Insurance { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Price { get; set; }

        public Policy(Quote quote)
        {
            Customer = quote.Customer;
            Insurance = quote.Insurance;
            Price = quote.Price;
            EffectiveDate = quote.EffectiveDate.ToDateTime(TimeOnly.MinValue);
            ExpirationDate = quote.ExpirationDate.ToDateTime(TimeOnly.MinValue);
            PolicyNumber = nextPolicyNumber++;

            Validate();
            quote.QuoteStatus = QuoteStatus.Issued;
        }

        public void Validate()
        {
            PolicyValidation.Validate(this);
            PolicyBusinessRules.EnforceBusinessRules(this);
        }

    }

}
