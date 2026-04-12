using InsuranceAPI.Models;
using InsuranceAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs.Requests
{
    public class UpdateQuoteRequest
    {
        public string? CustomerId { get; set; }

        [EnumDataType(typeof(InsuranceType))]
        public InsuranceType? InsuranceType { get; set; }

        public DateOnly? EffectiveDate { get; set; }

        public DateOnly? ExpirationDate { get; set; }


    }
}
