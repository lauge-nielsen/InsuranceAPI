namespace InsuranceAPI.Models
{
    public class InsurancePolicy
    {
        private static int nextPolicyNumber = 1;

        public int PolicyNumber { get; private set; }
        public string CustomerId { get; set; }
        public string InsuranceType { get; set; } 
        public double Price { get; set; }

        public InsurancePolicy(string customerId, string insuranceType, double price)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("CustomerId required");
            if (string.IsNullOrWhiteSpace(insuranceType))
                throw new ArgumentException("InsuranceType required");
            if (price < 0)
                throw new ArgumentException("Price cannot be negative");

            CustomerId = customerId;
            InsuranceType = insuranceType;
            Price = price;
            PolicyNumber = nextPolicyNumber++;
        }
    }
}
