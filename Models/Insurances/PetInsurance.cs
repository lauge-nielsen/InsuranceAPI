using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class PetInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.Pet;
        public string? PetName { get; set; } = request.PetName;
        public string? PetSpecies { get; set; } = request.PetSpecies;
        public DateOnly? PetDateOfBirth { get; set; } = request.PetDateOfBirth;
        public override double BasePrice { get; set; } = 300;
        public override void Validate(Customer customer)
        {

        }

        public override double ApplyPricing(Customer customer, double basePrice)
        {
            double price = basePrice;
            int? petAge = AgeInYears(PetDateOfBirth);

            if (petAge >= 10)
            {
                price *= 1.40;
            }
            else if (petAge >= 7)
            {
                price *= 1.20;
            }
            else if (petAge is >= 0 and < 1)
            {
                price *= 1.15;
            }

            string? species = PetSpecies?.Trim().ToLowerInvariant();

            if (species is "horse")
            {
                price *= 1.50;
            }
            else if (species is "dog")
            {
                price *= 1.20;
            }

            return price;
        }

        private static int? AgeInYears(DateOnly? dateOfBirth)
        {
            if (dateOfBirth is not DateOnly born)
            {
                return null;
            }

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - born.Year;

            if (born > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
