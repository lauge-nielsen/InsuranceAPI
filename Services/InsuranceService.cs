using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Services
{
    public class InsuranceService
    {
        // Simple in-memory storage for demo purposes
        public static List<Customer> customers = new();
        public static List<Quote> quotes = new();
        public static List<Policy> policies = new();

        public static Customer CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            Customer customer = new(request.CustomerId, request.Name, request.DateOfBirth);
            customers.Add(customer);

            return customer;
        }

        public static Quote CreateQuote([FromBody] CreateQuoteRequest request)
        {
            Quote quote = new(request.CustomerId, request.InsuranceType, request.EffectiveDate);
            quotes.Add(quote);

            return quote;
        }

        public static Policy? CreatePolicy([FromBody] CreatePolicyRequest request)
        {
            Quote quote = GetQuoteById(request.QuoteId);

            if (quote.QuoteStatus == QuoteStatus.Quoted)
            {
                Policy policy = new(quote);
                policies.Add(policy);
                quote.QuoteStatus = QuoteStatus.Issued;

                return policy;
            }

            else if (quote.QuoteStatus == QuoteStatus.Issued)
            {
                throw new ArgumentException("Quote is already issued");
            }

            return null;
        }

        public static Customer GetCustomerById(string customerId)
        {
            Customer? customer = customers.FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new ArgumentException("Customer not found");
            }

            return customer;
        }

        public static Quote GetQuoteById(string quoteId)
        {
            Quote? quote = quotes.FirstOrDefault(q => q.QuoteId == quoteId);

            if (quote == null)
            {
                throw new ArgumentException("Quote not found!");
            }

            return quote;
        }

        public static Policy GetPolicyByNumber(int policyNumber)
        {
            Policy? policy = policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);

            if (policy == null)
            {
                throw new ArgumentException("Policy not found!");
            }

            return policy;
        }

        public static Quote UpdateQuote(string quoteId, [FromBody] UpdateQuoteRequest request)
        {
            Quote quote = GetQuoteById(quoteId);

            if (quote.QuoteStatus != QuoteStatus.Quoted)
            {
                throw new InvalidOperationException("Only quotes with status 'Quoted' can be updated");
            }

            if (request.CustomerId != null)
                quote.Customer = InsuranceService.GetCustomerById(request.CustomerId);
            if (request.InsuranceType.HasValue)
                quote.InsuranceType = request.InsuranceType.Value;
            if (request.EffectiveDate.HasValue)
                quote.EffectiveDate = request.EffectiveDate.Value;
            if (request.ExpirationDate.HasValue)
                quote.ExpirationDate = request.ExpirationDate.Value;
            if (request.Price.HasValue)
                quote.Price = request.Price.Value;

            quote.ValidateQuote(); //TODO: Ensure a quote is not updated unless all changes are valid

            return quote;
        }

        public static void DeleteQuote(string quoteId)
        {
            Quote quote = GetQuoteById(quoteId);
            quotes.Remove(quote);
        }

        public static double CalculatePrice(Customer customer, InsuranceType insuranceType)
        {
            double basePrice = insuranceType == InsuranceType.Car ? 1000 : 500;
            int age = customer.Age();

            if (age < 25)
                basePrice *= 1.5;

            if (age > 60)
                basePrice *= 1.2;

            if (basePrice < 0)
            {
                throw new InvalidOperationException("Calculated price cannot be negative");
            }

            return basePrice;
        }
    }
}
