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

        public static Customer CreateCustomer(CreateCustomerRequest request)
        {
            Customer customer = new(request.CustomerId, request.Name, request.DateOfBirth);
            customers.Add(customer);

            return customer;
        }

        public static Quote CreateQuote(CreateQuoteRequest request)
        {
            Quote quote = new(request.CustomerId, request.InsuranceType, request.EffectiveDate);
            quote.Validate();
            quotes.Add(quote);

            return quote;
        }

        public static Policy CreatePolicy(CreatePolicyRequest request)
        {
            Quote quote = GetQuoteById(request.QuoteId)!;

            if (quote.QuoteStatus == QuoteStatus.Quoted)
            {
                Policy policy = new(quote);
                policies.Add(policy);
                quote.QuoteStatus = QuoteStatus.Issued;

                return policy;
            }

            else
            {
                throw new ArgumentException("QuoteStatus is not 'Quoted'");
            }

        }

        public static List<Customer> GetCustomers => customers;

        public static Customer? GetCustomerById(string customerId)
        {
            if (customerId == null)
            {
                return null;
            }

            return GetCustomers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public static Quote? GetQuoteById(string quoteId)
        {

            if (quoteId == null)
            {
                return null;
            }

            return quotes.FirstOrDefault(q => q.QuoteId == quoteId);
        }

        public static Policy? GetPolicyByNumber(int policyNumber)
        {
            Policy? policy = policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
            return policy;
        }

        public static Quote UpdateQuote(string quoteId, UpdateQuoteRequest request)
        {
            Quote quote = GetQuoteById(quoteId) ?? throw new ArgumentException("Quote not found");

            if (quote.QuoteStatus != QuoteStatus.Quoted)
            {
                throw new InvalidOperationException("Only quotes with status 'Quoted' can be updated");
            }

            Quote proposedQuote = new()
            {
                QuoteId = quoteId,
                Customer = request.CustomerId == null 
                            ? quote.Customer 
                            : InsuranceService.GetCustomerById(request.CustomerId) ?? throw new ArgumentException("Customer not found"),
                InsuranceType = request.InsuranceType ?? quote.InsuranceType,
                EffectiveDate = request.EffectiveDate ?? quote.EffectiveDate,
                ExpirationDate = request.ExpirationDate ?? quote.ExpirationDate,
                Price = quote.Price,
                QuoteStatus = quote.QuoteStatus,
            };

            proposedQuote.Validate();
            proposedQuote.Price = CalculatePrice(proposedQuote.Customer, proposedQuote.InsuranceType);

            quote.Customer = proposedQuote.Customer;
            quote.InsuranceType = proposedQuote.InsuranceType;
            quote.EffectiveDate = proposedQuote.EffectiveDate;
            quote.ExpirationDate = proposedQuote.ExpirationDate;
            quote.Price = proposedQuote.Price;

            return quote;
        }

        public static void DeleteQuote(string quoteId)
        {
            Quote? quote = GetQuoteById(quoteId) ?? throw new ArgumentException("Quote not found");
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

            if (basePrice <= 0)
            {
                throw new InvalidOperationException("Calculated price cannot be zero or negative");
            }

            return basePrice;
        }
    }
}
