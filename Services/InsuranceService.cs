using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Services
{
    public class InsuranceService
    {
        // Simple in-memory storage for demo purposes
        public static List<Customer> customers = new();
        public static List<Policy> policies = new();

        public Customer CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            if (customers.FirstOrDefault(c => c.CustomerId == request.CustomerId) != null)
            {
                throw new ArgumentException("The customer already exists");
            }

            Customer customer = new(request.CustomerId, request.Name, request.DateOfBirth);

            if (customer.Age() < 18)
            {
                throw new ArgumentException("The customer must be at least 18 years old");
            }

            customers.Add(customer);
            return customer;
        }

        public Policy CreatePolicy([FromBody] CreatePolicyRequest request)
        {
            string insuranceType = request.InsuranceType;
            string customerId = request.CustomerId;
            Customer? customer = GetCustomerById(customerId) ?? throw new ArgumentException("Customer not found");

            if (insuranceType == "Car" && customer.Age() < 20)
            {
                throw new ArgumentException("The customer must be at least 20 years old to purchase a car insurance");
            }

            double price = CalculatePrice(customer, insuranceType);
            Policy policy = new(customerId, insuranceType, price);

            policies.Add(policy);
            return policy;
        }

        public Customer? GetCustomerById(string customerId)
        {
            Customer? customer = customers.FirstOrDefault(c => c.CustomerId == customerId);
            return customer;
        }

        public Policy? GetPolicyByNumber(int policyNumber)
        {
            Policy? policy = policies.FirstOrDefault(p => p.PolicyNumber == policyNumber);
            return policy;
        }

        public double CalculatePrice(Customer customer, string insuranceType)
        {
            double basePrice = insuranceType == "Car" ? 1000 : 500;
            int age = customer.Age();

            if (age < 25)
                basePrice *= 1.5;

            if (age > 60)
                basePrice *= 1.2;

            return basePrice;
        }
    }
}
