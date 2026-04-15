using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;

namespace InsuranceAPI.DTOs.Responses
{
    public class InsuranceResponse
    {
        public InsuranceType InsuranceType { get; set; }

        //HouseInsurance fields;
        public int? HouseYearBuilt { get; set; } = null;
        public int? HouseArea { get; set; } = null;


        // PetInsurance fields
        public string? PetName { get; set; } = null;
        public string? PetSpecies { get; set; } = null;
        public DateOnly? PetDateOfBirth { get; set; } = null;

        public InsuranceResponse(Insurance insurance)
        {
            InsuranceType = insurance.InsuranceType;

            if (insurance is HouseInsurance)
            {
                HouseYearBuilt = ((HouseInsurance)insurance).HouseYearBuilt;
                HouseArea = ((HouseInsurance)insurance).HouseArea;
            }
            
            if (insurance is PetInsurance)
            {
                PetName = ((PetInsurance)insurance).PetName;
                PetSpecies  = ((PetInsurance)insurance).PetSpecies;
                PetDateOfBirth = ((PetInsurance)insurance).PetDateOfBirth;
            };

        }

    }
}
