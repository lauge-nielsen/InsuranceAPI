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
            return Ok(CustomerService.GetCustomers);
        }

        [HttpGet("customer/{customerId}")]
        public ActionResult<Customer> GetCustomerById(string customerId)
        {
            Customer? customer = CustomerService.FindCustomerById(customerId);

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
                Customer customer = CustomerService.CreateCustomer(request);
                return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.CustomerId }, customer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("customer/{customerId}")]
        public ActionResult<Customer> UpdateCustomer(string customerId, [FromBody] UpdateCustomerRequest request)
        {
            try
            {
                Customer customer = CustomerService.UpdateCustomer(customerId, request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

}
