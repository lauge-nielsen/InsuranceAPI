using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class TravelInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.Travel;
        public int? DurationDays { get; set; } = request.TravelDurationDays;
        public override double BasePrice { get; set; } = 350;
        public override void Validate(Customer customer)
        {

        }

        public override double ApplyPricing(Customer customer, double basePrice)
        {
            double price = basePrice;

            if (DurationDays >= 90)
            {
                price *= 1.50;
            }
            else if (DurationDays >= 30)
            {
                price *= 1.25;
            }
            else if (DurationDays >= 14)
            {
                price *= 1.10;
            }

            return price;
        }
    }
}
