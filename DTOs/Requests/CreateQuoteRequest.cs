using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs.Requests
{
    public class CreateQuoteRequest
    {
        [Required]
        public required string CustomerId { get; set; }

        [Required]
        public required DateOnly EffectiveDate { get; set; }

        [Required]
        public required InsuranceRequest InsuranceRequest { get; set; }

    }

}
