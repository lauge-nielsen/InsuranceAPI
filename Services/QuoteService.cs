using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;

namespace InsuranceAPI.Services
{
    public class QuoteService
    {
        // Simple in-memory storage for demo purposes
        public static List<Quote> quotes = new();

        public static Quote CreateQuote(CreateQuoteRequest request)
        {
            ValidateCreateQuoteRequest(request);

            Customer customer = CustomerService.GetCustomerById(request.CustomerId);
            double price = PricingService.CalculatePrice(customer, request.InsuranceType);
            Quote quote = new(customer, request.InsuranceType, request.EffectiveDate, price);
            quotes.Add(quote);

            return quote;
        }

        public static Quote GetQuoteById(string quoteId)
        {
            return FindQuoteById(quoteId) ?? throw new InvalidOperationException("Quote not found");
        }

        public static Quote? FindQuoteById(string quoteId)
        {
            return quotes.FirstOrDefault(q => q.QuoteId == quoteId);
        }

        public static Quote UpdateQuote(string quoteId, UpdateQuoteRequest request)
        {
            Quote quote = GetQuoteById(quoteId);

            if (quote.QuoteStatus != QuoteStatus.Quoted)
            {
                throw new InvalidOperationException("Only quotes with status 'Quoted' can be updated");
            }

            Quote proposedQuote = new()
            {
                QuoteId = quoteId,
                Customer = request.CustomerId == null 
                            ? quote.Customer 
                            : CustomerService.FindCustomerById(request.CustomerId) ?? throw new InvalidOperationException("Customer not found"),
                InsuranceType = request.InsuranceType ?? quote.InsuranceType,
                EffectiveDate = request.EffectiveDate ?? quote.EffectiveDate,
                ExpirationDate = request.ExpirationDate ?? quote.ExpirationDate,
                QuoteStatus = quote.QuoteStatus,
            };

            proposedQuote.Price = PricingService.CalculatePrice(proposedQuote.Customer, proposedQuote.InsuranceType);
            proposedQuote.Validate();

            quote.Customer = proposedQuote.Customer;
            quote.InsuranceType = proposedQuote.InsuranceType;
            quote.EffectiveDate = proposedQuote.EffectiveDate;
            quote.ExpirationDate = proposedQuote.ExpirationDate;
            quote.Price = proposedQuote.Price;

            return quote;
        }

        public static Quote AcceptQuote(string quoteId)
        {
            Quote quote = GetQuoteById(quoteId);
            QuoteBusinessRules.ValidateCanBeAccepted(quote);
            quote.Validate();
            
            quote.QuoteStatus = QuoteStatus.Quoted;

            return quote;
        }

        public static void DeleteQuote(string quoteId)
        {
            Quote quote = GetQuoteById(quoteId);
            quotes.Remove(quote);
        }

        private static void ValidateCreateQuoteRequest(CreateQuoteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerId))
                throw new ArgumentException("CustomerId required");

            if (request.EffectiveDate < DateOnly.FromDateTime(DateTime.UtcNow.Date))
                throw new ArgumentException("EffectiveDate cannot be in the past");
        }

    }

}
