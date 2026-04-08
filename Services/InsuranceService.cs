using InsuranceAPI.Models;

namespace InsuranceAPI.Services
{
    public class InsuranceService
    {
        public double CalculatePrice(Customer customer, string insuranceType)
        {
            double basePrice = insuranceType == "Car" ? 1000 : 500;
            int age = customer.Age();

            if (age < 25)
                basePrice *= 1.5;

            if (age > 60)
                basePrice *= 1.2;

            return basePrice;
        }
    }
}
