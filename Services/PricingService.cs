using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;

namespace InsuranceAPI.Services
{
    public class PricingService
    {
        public static double CalculatePrice(Customer customer, Insurance insurance)
        {
            double basePrice = CalculateBasePrice(customer, insurance);
            double finalPrice = insurance.ApplyPricing(customer, basePrice);

            ValidatePrice(finalPrice);

            return finalPrice;
        }

        public static double CalculateBasePrice(Customer customer, Insurance insurance)
        {
            double basePrice = insurance.BasePrice;

            int age = customer.Age();
            if (age < 25)
                basePrice *= 1.5;
            if (age > 60)
                basePrice *= 1.2;

            return basePrice;
        }

        public static void ValidatePrice(double price)
        {
            if (price <= 0)
            {
                throw new InvalidOperationException("Calculated price cannot be zero or negative");
            }

        }

    }

}
