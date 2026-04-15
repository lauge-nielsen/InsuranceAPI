using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Services.Factories;

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
            Quote quote = new(customer, request.InsuranceRequest, request.EffectiveDate);
            quote.Price = PricingService.CalculatePrice(customer, quote.Insurance);
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

            if (quote.QuoteStatus != QuoteStatus.Draft)
            {
                throw new InvalidOperationException("Only quotes with status 'Draft' can be updated");
            }

            Quote proposedQuote = new()
            {
                QuoteId = quoteId,
                Customer = request.CustomerId == null 
                            ? quote.Customer 
                            : CustomerService.FindCustomerById(request.CustomerId) ?? throw new InvalidOperationException("Customer not found"),
                Insurance = request.InsuranceRequest is not null ? InsuranceFactory.Create(request.InsuranceRequest) : quote.Insurance,
                EffectiveDate = request.EffectiveDate ?? quote.EffectiveDate,
                ExpirationDate = request.ExpirationDate ?? quote.ExpirationDate,
                QuoteStatus = quote.QuoteStatus,
            };

            proposedQuote.Price = PricingService.CalculatePrice(proposedQuote.Customer, proposedQuote.Insurance);
            proposedQuote.Validate();

            quote.Customer = proposedQuote.Customer;
            quote.Insurance = proposedQuote.Insurance;
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
