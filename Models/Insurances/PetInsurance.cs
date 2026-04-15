using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class PetInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.Pet;
        public string? PetName { get; set; } = request.PetName;
        public string? PetSpecies { get; set; } = request.PetSpecies;
        public DateOnly? PetDateOfBirth { get; set; } = request.PetDateOfBirth;

        public override void Validate(Customer customer)
        {

        }
    }
}
