using InsuranceAPI.DTOs.Requests;

namespace InsuranceAPI.Models.Insurances
{
    public class AccidentInsurance(InsuranceRequest request) : Insurance(request)
    {
        public new InsuranceType InsuranceType => InsuranceType.Accident;
        public override double BasePrice { get; set; } = 400;

        public override void Validate(Customer customer)
        {

        }

        public override double ApplyPricing(Customer customer, double basePrice)
        {
            double price = basePrice;
            string? occupation = customer.Occupation?.ToLowerInvariant();

            if (ContainsAny(occupation, "construction", "miner", "mining", "pilot", "firefighter", "fisher", "offshore", "roofer"))
            {
                price *= 1.35;
            }
            else if (ContainsAny(occupation, "driver", "farmer", "mechanic", "police", "electrician"))
            {
                price *= 1.15;
            }

            return price;
        }

        private static bool ContainsAny(string? value, params string[] terms)
        {
            if (value is null)
            {
                return false;
            }

            foreach (string term in terms)
            {
                if (value.Contains(term, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }
    }
}