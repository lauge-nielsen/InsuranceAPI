using InsuranceAPI.Models;

namespace InsuranceAPI.Domain.Validation
{
    public static class CustomerValidation
    {
        public static void Validate(Customer customer)
        {
            ValidateStructure(customer);
        }

        public static void ValidateStructure(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerId))
                throw new ArgumentException("CustomerId required");

            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Name required");

        }
    }
}
