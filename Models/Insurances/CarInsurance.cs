using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class CarInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.Car;

        public int? KilometersDriven { get; set; } = request.CarKilometersDriven;
        public override double BasePrice { get; set; } = 800;

        public override void Validate(Customer customer)
        {

        }

        public override double ApplyPricing(Customer customer, double basePrice)
        {
            double price = basePrice;

            if (KilometersDriven >= 100000)
            {
                price *= 1.1;
            }

            else if (KilometersDriven >= 150000)
            {
                price *= 1.15;
            }

            else if (KilometersDriven >= 200000)
            {
                price *= 1.20;
            }

            return price;
        }

    }
}
