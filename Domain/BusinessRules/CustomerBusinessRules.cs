using InsuranceAPI.Models;

namespace InsuranceAPI.Domain.BusinessRules
{
    public static class CustomerBusinessRules
    {
        public static void EnforceBusinessRules(Customer customer)
        {
            if (customer.DateOfBirth > DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18))
                throw new ArgumentException("Customer must be at least 18 years old");
        }

    }

}
