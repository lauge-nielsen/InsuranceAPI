using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class HouseInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.House;
        public int? HouseYearBuilt { get; set; } = request.HouseYearBuilt;
        public int? HouseArea { get; set; } = request?.HouseArea;
        public override double BasePrice { get; set; } = 1500;
        public override void Validate(Customer customer)
        {

        }

        public override double ApplyPricing(Customer customer, double basePrice)
        {
            double price = basePrice;,

            if (HouseArea <= 100)
            {
                price *= 1.3;
            }

            return price;
        }
    }
}
