using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;

namespace InsuranceAPI.Services
{
    public class CustomerService
    {
        // Simple in-memory storage for demo purposes
        public static List<Customer> customers = new();

        public static Customer CreateCustomer(CreateCustomerRequest request)
        {
            if (customers.Any(c => c.CustomerId == request.CustomerId))
                throw new ArgumentException("CustomerId must be unique");

            Customer customer = new(request.CustomerId, request.Name, request.DateOfBirth);
            customers.Add(customer);

            return customer;
        }

        public static Customer UpdateCustomer(string customerId, UpdateCustomerRequest request)
        {
            Customer customer = GetCustomerById(customerId);

            Customer proposedCustomer = new()
            {
                CustomerId = customerId,
                Name = request.Name ?? customer.Name,
                DateOfBirth = request.DateOfBirth ?? customer.DateOfBirth
            };

            proposedCustomer.Validate();
            
            customer.Name = proposedCustomer.Name;
            customer.DateOfBirth = proposedCustomer.DateOfBirth;

            return customer;
        }

        public static List<Customer> GetCustomers => customers;

        public static Customer GetCustomerById(string customerId)
        {
            return FindCustomerById(customerId) ?? throw new InvalidOperationException("Customer not found");
        }

        public static Customer? FindCustomerById(string customerId)
        {
            return customers.FirstOrDefault(c => c.CustomerId == customerId);
        }
    }
}
