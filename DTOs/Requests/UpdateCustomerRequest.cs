namespace InsuranceAPI.DTOs.Requests
{
    public class UpdateCustomerRequest
    {
        public string? Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public int? YearsOfDrivingExperience { get; set; }
        public int? NumberOfDrivingfAccidents { get; set; }
    }

}