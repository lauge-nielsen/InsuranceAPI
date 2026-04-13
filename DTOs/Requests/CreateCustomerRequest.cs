using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.DTOs.Requests
{
    public class CreateCustomerRequest
    {
        [Required]
        public required string CustomerId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required DateOnly DateOfBirth { get; set; }

    }

}
