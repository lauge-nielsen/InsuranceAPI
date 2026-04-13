using InsuranceAPI.Models;

namespace InsuranceAPI.Domain.Validation
{
    public static class QuoteValidation
    {
        public static void ValidateStructure(Quote quote)
        {
            if (string.IsNullOrWhiteSpace(quote.QuoteId))
                throw new ArgumentException("QuoteId required");

            if (quote.Customer == null)
                throw new ArgumentException("Customer required");

            if (quote.EffectiveDate < DateOnly.FromDateTime(DateTime.UtcNow.Date))
                throw new ArgumentException("EffectiveDate cannot be in the past");

            if (quote.ExpirationDate <= quote.EffectiveDate)
                throw new ArgumentException($"Invalid date range: EffectiveDate={quote.EffectiveDate}, ExpirationDate={quote.ExpirationDate}");
        }

    }

}
