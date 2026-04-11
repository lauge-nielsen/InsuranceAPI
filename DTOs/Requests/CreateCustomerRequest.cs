namespace InsuranceAPI.DTOs.Requests
{
    public class CreateCustomerRequest
    {
        public required string CustomerId { get; set; }
        public required string Name { get; set; } 
        public required DateOnly DateOfBirth { get; set; }

    }
}
