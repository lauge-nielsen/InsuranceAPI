using InsuranceAPI.Models;
using Microsoft.Extensions.Diagnostics.Metrics;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs.Requests
{
    public class CreateQuoteRequest
    {
        [Required]
        public required string CustomerId { get; set; }

        [Required]
        [EnumDataType(typeof(InsuranceType))]
        public required InsuranceType InsuranceType { get; set; }

        [Required]
        public required DateOnly EffectiveDate { get; set; }
    }
}
