using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;

namespace InsuranceAPI.Services
{
    public class PolicyService
    {
        // Simple in-memory storage for demo purposes
        public static List<Policy> policies = new();

        public static Policy CreatePolicy(CreatePolicyRequest request)
        {
            ValidateCreatePolicyRequest(request);

            Quote quote = QuoteService.GetQuoteById(request.QuoteId);

            if (quote.QuoteStatus is QuoteStatus.Quoted)
            {
                QuoteBusinessRules.ValidateCanBeIssued(quote);
                quote.Validate();
                Policy policy = new(quote);
                policies.Add(policy);

                return policy;
            }

            else
            {
                throw new InvalidOperationException("QuoteStatus is not 'Quoted'");
            }

        }

        public static Policy GetPolicyByNumber(int policyNumber)
        {
            return FindPolicyByNumber(policyNumber) ?? throw new InvalidOperationException("Policy not found");
        }

        public static Policy? FindPolicyByNumber(int policyNumber)
        {
            return policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
        }

        public static void ValidateCreatePolicyRequest(CreatePolicyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.QuoteId))
                throw new ArgumentException("QuoteId required");
        }

    }

}
