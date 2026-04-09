namespace InsuranceAPI.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }

        public string Name { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Customer(string id, string name, DateOnly dateOfBirth)
        {
            CustomerId = id;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

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
