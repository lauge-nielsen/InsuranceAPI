using InsuranceAPI.Models;

namespace InsuranceAPI.Domain.BusinessRules
{
    public static class QuoteBusinessRules
    {
        public static void EnforceBusinessRules(Quote quote) {
            ValidateCustomerEligibility(quote);
        }

        public static void ValidateCustomerEligibility(Quote quote)
        {
            if (quote.Customer.Age() < 20 && quote.Insurance.InsuranceType is InsuranceType.Spacecraft)
                throw new InvalidOperationException("Customer must be at least 20 years old to purchase a Spacecraft insurance");
            
            if (quote.Customer.Age() > 79 && quote.Insurance.InsuranceType is InsuranceType.Accident)
                throw new InvalidOperationException("Customer must be at most 79 years old to purchase an Accident insurance");
        }


        public static void ValidateCanBeAccepted(Quote quote)
        {
            if (quote.QuoteStatus is not QuoteStatus.Draft)
                throw new InvalidOperationException("Quote must be in 'Draft' status to be accepted");
        }

        public static void ValidateCanBeIssued(Quote quote)
        {
            if (quote.QuoteStatus is not QuoteStatus.Quoted)
                throw new InvalidOperationException("Quote must be in 'Quoted' status to be issued");
        }

    }

}
