namespace InsuranceAPI.DTOs.Requests
{
    public class UpdateCustomerRequest
    {
        public string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
    }

}