using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;

namespace InsuranceAPI.DTOs.Responses
{
    public class QuoteResponse(Quote quote)
    {
        public string QuoteId { get; set; } = quote.QuoteId;
        public Customer Customer { get; set; } = quote.Customer;
        public DateOnly EffectiveDate { get; set; } = quote.EffectiveDate;
        public DateOnly ExpirationDate { get; set; } = quote.ExpirationDate;
        public InsuranceResponse Insurance { get; set; } = new(quote.Insurance);
        public double Price { get; set; } = quote.Price;
    }
}
