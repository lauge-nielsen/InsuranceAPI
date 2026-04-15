using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class Insurance(InsuranceRequest request)
    {
        public InsuranceType InsuranceType { get; set; } = request.InsuranceType;
        public virtual void Validate(Customer customer)
        {

        }

    }
}
