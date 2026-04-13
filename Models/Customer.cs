using InsuranceAPI.Domain.BusinessRules;
using InsuranceAPI.Domain.Validation;
using InsuranceAPI.Services;

namespace InsuranceAPI.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }

        public string Name { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public Customer(string customerId, string name, DateOnly dateOfBirth)
        {
            CustomerId = customerId;
            Name = name;
            DateOfBirth = dateOfBirth;

            Validate();
        }

        public Customer()
        {

        }

        public void Validate() 
        { 
            CustomerValidation.Validate(this);
            CustomerBusinessRules.EnforceBusinessRules(this);
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
