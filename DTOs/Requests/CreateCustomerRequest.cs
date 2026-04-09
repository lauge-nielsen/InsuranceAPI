namespace InsuranceAPI.DTOs.Requests
{
    public class CreateCustomerRequest(string customerId, string name, DateOnly dateOfBirth)
    {
        public string CustomerId { get; set; } = customerId;
        public string Name { get; set; } = name;
        public DateOnly DateOfBirth { get; set; } = dateOfBirth;

    }
}
