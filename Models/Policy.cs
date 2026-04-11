namespace InsuranceAPI.Models
{
    public class Policy
    {
        private static int nextPolicyNumber = 1;

        public int PolicyNumber { get; private set; }
        public Customer Customer { get; set; }
        public InsuranceType InsuranceType { get; set; } 
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Price { get; set; }

        public Policy(Quote quote)
        {
            ValidatePolicyData(quote.Customer, quote.InsuranceType, quote.Price);

            Customer = quote.Customer;
            InsuranceType = quote.InsuranceType;
            Price = quote.Price;
            EffectiveDate = quote.EffectiveDate.ToDateTime(TimeOnly.MinValue);
            ExpirationDate = quote.ExpirationDate.ToDateTime(TimeOnly.MinValue);
            PolicyNumber = nextPolicyNumber++;
        }

        private static void ValidatePolicyData(Customer customer, InsuranceType insuranceType, double price)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerId))
                throw new ArgumentException("CustomerId required");
            if (!Enum.IsDefined(typeof(InsuranceType), insuranceType))
                throw new ArgumentException("InsuranceType required");
            if (price < 0)
                throw new ArgumentException("Price cannot be negative");
            if (insuranceType == InsuranceType.Car && customer.Age() < 20)
                throw new ArgumentException("Customer must be at least 20 years old to purchase a Car insurance");
        }
    }
}
