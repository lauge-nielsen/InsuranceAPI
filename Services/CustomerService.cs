using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Services
{
    public class CustomerService
    {
        // Simple in-memory storage for demo purposes
        public static List<Customer> customers = new();

        public static Customer CreateCustomer(CreateCustomerRequest request)
        {
            Customer customer = new(request.CustomerId, request.Name, request.DateOfBirth);
            customers.Add(customer);

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
