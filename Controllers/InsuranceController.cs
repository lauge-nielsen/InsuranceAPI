using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly InsuranceService _service;

        // Simple in-memory storage for demo purposes
        private static List<Customer> customers = new();

        public InsuranceController(InsuranceService service)
        {
            _service = service;
        }

        [HttpGet("price")]
        public double GetPrice(Customer customer, string insuranceType)
        {
            return _service.CalculatePrice(customer, insuranceType);
        }

        [HttpGet("customer")]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(customers);
        }

        [HttpPost("customer")]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer customer) 
        {
            if (customer.Age() < 18)
            {
                return BadRequest("Customer must be at least 18 years old");
            }

            customers.Add(customer);
            return Ok(customer);
        }

        [HttpPost("policy")]
        public ActionResult<InsurancePolicy> CreatePolicy([FromBody] Customer customer, string insuranceType)
        {
            var price = _service.CalculatePrice(customer, insuranceType);

            if (insuranceType == "Car" && customer.Age() < 20)
            {
                return BadRequest("The customer is too young to purchase a car insurance");
            }

            InsurancePolicy policy = new InsurancePolicy(customer.Id, insuranceType, price);

            return Ok(policy);
        }
    }
}
