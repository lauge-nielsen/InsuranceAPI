using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.Domain.Validation;

namespace InsuranceAPI.Models
{
    public class Policy
    {
        private static int nextPolicyNumber = 1;

        public int PolicyNumber { get; private set; }
        public Customer Customer { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Price { get; set; }

        public Policy(Quote quote)
        {
            Customer = quote.Customer;
            InsuranceType = quote.InsuranceType;
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
