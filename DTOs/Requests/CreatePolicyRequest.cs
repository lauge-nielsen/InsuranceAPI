using InsuranceAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs.Requests
{
    public class CreatePolicyRequest
    {
        [Required]
        public required string QuoteId { get; set; }
    }
}
