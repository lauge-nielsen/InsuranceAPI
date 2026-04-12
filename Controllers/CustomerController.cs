using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    public class CustomerController() : ControllerBase
    {
       
        [HttpGet("customer")]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(InsuranceService.GetCustomers);
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<Customer> GetCustomerById(string customerId)
        {
            Customer? customer = InsuranceService.GetCustomerById(customerId);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customer);
        }

        [HttpPost("customer")]
        public ActionResult<Customer> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            try
            {
                Customer customer = InsuranceService.CreateCustomer(request);
                return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.CustomerId }, customer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
