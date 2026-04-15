using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class HouseInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.House;
        public int? HouseYearBuilt { get; set; } = request.HouseYearBuilt;
        public int? HouseArea { get; set; } = request?.HouseArea;
        public override void Validate(Customer customer)
        {

        }
    }
}
