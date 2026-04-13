using InsuranceAPI.Services;

namespace InsuranceAPI.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }

        public string Name { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Customer(string id, string name, DateOnly dateOfBirth)
        {
            ValidateCustomerData(id, name, dateOfBirth);

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

        private static void ValidateCustomerData(string id, string name, DateOnly dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("CustomerId required");
            if (CustomerService.customers != null && CustomerService.customers.Any(c => c.CustomerId == id))
                throw new ArgumentException("CustomerId must be unique");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name required");
            if (dateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18))
                throw new ArgumentException("Customer must be at least 18 years old");
        }
    }
}
