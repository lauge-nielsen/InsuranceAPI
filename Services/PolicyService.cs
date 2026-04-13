using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Services
{
    public class PolicyService
    {
        // Simple in-memory storage for demo purposes
        public static List<Policy> policies = new();

        public static Policy CreatePolicy(CreatePolicyRequest request)
        {
            Quote quote = QuoteService.GetQuoteById(request.QuoteId);

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

        public static Policy GetPolicyByNumber(int policyNumber)
        {
            return FindPolicyByNumber(policyNumber) ?? throw new InvalidOperationException("Policy not found");
        }

        public static Policy? FindPolicyByNumber(int policyNumber)
        {
            return policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
        }
    }
}
