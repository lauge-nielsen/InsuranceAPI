using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class SpacecraftInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.Spacecraft;
        public int? YearlyFlightHours { get; set; } = request.SpacecraftYearlyFlightHours;
        public override double BasePrice { get; set; } = 3500;
        public override void Validate(Customer customer)
        {

        }

        public override double ApplyPricing(Customer customer, double basePrice)
        {
            double price = basePrice;

            if (YearlyFlightHours >= 20000)
            {
                price *= 1.30;
            }
            else if (YearlyFlightHours >= 10000)
            {
                price *= 1.20;
            }
            else if (YearlyFlightHours >= 5000)
            {
                price *= 1.10;
            }

            return price;
        }
    }
}
