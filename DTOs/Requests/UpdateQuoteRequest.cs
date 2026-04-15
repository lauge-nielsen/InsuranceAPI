using InsuranceAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs.Requests
{
    public class UpdateQuoteRequest
    {
        public string? CustomerId { get; set; }

        public InsuranceRequest? InsuranceRequest { get; set; }

        public DateOnly? EffectiveDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }

    }

}
