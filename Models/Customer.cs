namespace InsuranceAPI.Models
{
    public class Customer
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public int Age()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            int age = today.Year - DateOfBirth.Year;

            if (DateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
