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
            double price = basePrice;

            if (HouseArea >= 250)
            {
                price *= 1.30;
            }
            else if (HouseArea >= 150)
            {
                price *= 1.15;
            }
            else if (HouseArea >= 80)
            {
                price *= 1.05;
            }

            int? buildingAge = HouseYearBuilt is int yearBuilt
                ? DateTime.UtcNow.Year - yearBuilt
                : null;

            if (buildingAge >= 75)
            {
                price *= 1.25;
            }
            else if (buildingAge >= 45)
            {
                price *= 1.15;
            }
            else if (buildingAge >= 25)
            {
                price *= 1.10;
            }

            return price;
        }
    }
}
