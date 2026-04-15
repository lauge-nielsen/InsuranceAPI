using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;

namespace InsuranceAPI.Services
{
    public class PricingService
    {
        public static double CalculatePrice(Customer customer, Insurance insurance)
        {
            double basePrice = insurance is SpacecraftInsurance ? 1000 : 500;
            int age = customer.Age();

            if (age < 25)
                basePrice *= 1.5;

            if (age > 60)
                basePrice *= 1.2;

            if (basePrice <= 0)
            {
                throw new InvalidOperationException("Calculated price cannot be zero or negative");
            }

            return basePrice;
        }

    }

}
