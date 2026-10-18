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

        // CarInsurance fields
        public int? CarKilometersDriven { get; set; } = null;

        // TravelInsurance fields
        public int? TravelDurationDays { get; set; } = null;

        // SpacecraftInsurance fields
        public int? SpacecraftFlightHours { get; set; } = null;

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

            if (insurance is CarInsurance)
            {
                CarKilometersDriven = ((CarInsurance)insurance).KilometersDriven;
            }

            if (insurance is TravelInsurance)
            {
                TravelDurationDays = ((TravelInsurance)insurance).DurationDays;
            }

            if (insurance is SpacecraftInsurance)
            {
                SpacecraftFlightHours = ((SpacecraftInsurance)insurance).YearlyFlightHours;
            }

        }

    }
}
