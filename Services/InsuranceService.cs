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
            ValidateCreateQuoteRequest(request);

            Customer customer = GetCustomerById(request.CustomerId);
            double price = CalculatePrice(customer, request.InsuranceType);
            Quote quote = new(customer, request.InsuranceType, request.EffectiveDate, price);
            quotes.Add(quote);

            return quote;
        }

        private static void ValidateCreateQuoteRequest(CreateQuoteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerId))
                throw new ArgumentException("CustomerId required");

            if (request.EffectiveDate < DateOnly.FromDateTime(DateTime.UtcNow.Date))
                throw new ArgumentException("EffectiveDate cannot be in the past");
        }

        public static Policy CreatePolicy(CreatePolicyRequest request)
        {
            Quote quote = GetQuoteById(request.QuoteId);

            if (quote.QuoteStatus is QuoteStatus.Quoted)
            {
                Policy policy = new(quote);
                policies.Add(policy);
                quote.QuoteStatus = QuoteStatus.Issued;

                return policy;
            }

            else
            {
                throw new InvalidOperationException("QuoteStatus is not 'Quoted'");
            }

        }

        public static List<Customer> GetCustomers => customers;

        public static Customer GetCustomerById(string customerId)
        {
            return FindCustomerById(customerId) ?? throw new InvalidOperationException("Customer not found");
        }

        public static Customer? FindCustomerById(string customerId)
        {
            return customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public static Quote GetQuoteById(string quoteId)
        {
            return FindQuoteById(quoteId) ?? throw new InvalidOperationException("Quote not found");
        }

        public static Quote? FindQuoteById(string quoteId)
        {
            return quotes.FirstOrDefault(q => q.QuoteId == quoteId);
        }

        public static Policy GetPolicyByNumber(int policyNumber)
        {
            return FindPolicyByNumber(policyNumber) ?? throw new InvalidOperationException("Policy not found");
        }

        public static Policy? FindPolicyByNumber(int policyNumber)
        {
            return policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
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
                            : FindCustomerById(request.CustomerId) ?? throw new InvalidOperationException("Customer not found"),
                InsuranceType = request.InsuranceType ?? quote.InsuranceType,
                EffectiveDate = request.EffectiveDate ?? quote.EffectiveDate,
                ExpirationDate = request.ExpirationDate ?? quote.ExpirationDate,
                QuoteStatus = quote.QuoteStatus,
            };

            proposedQuote.Price = CalculatePrice(proposedQuote.Customer, proposedQuote.InsuranceType);
            proposedQuote.Validate();

            quote.Customer = proposedQuote.Customer;
            quote.InsuranceType = proposedQuote.InsuranceType;
            quote.EffectiveDate = proposedQuote.EffectiveDate;
            quote.ExpirationDate = proposedQuote.ExpirationDate;
            quote.Price = proposedQuote.Price;

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

            if (basePrice <= 0)
            {
                throw new InvalidOperationException("Calculated price cannot be zero or negative");
            }

            return basePrice;
        }
    }
}
