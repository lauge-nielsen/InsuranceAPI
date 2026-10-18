using InsuranceAPI.Models;

namespace InsuranceAPI.DTOs.Requests
{
    public class InsuranceRequest
    {
        public required InsuranceType InsuranceType { get; set; }

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
        public int? SpacecraftYearlyFlightHours { get; set; } = null;

    }
}
