using InsuranceAPI.DTOs.Requests;
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

        public InsuranceController(InsuranceService service)
        {
            _service = service;
        }

        [HttpGet("price")]
        [ActionName("GetPrice")]
        public double GetPrice(Customer customer, string insuranceType)
        {
            return _service.CalculatePrice(customer, insuranceType);
        }

        [HttpGet("customer")]
        [ActionName("GetCustomers")]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(InsuranceService.customers);
        }

        [HttpGet("policy")]
        [ActionName("GetPolicies")]
        public ActionResult<List<Policy>> GetPolicies()
        {
            return Ok(InsuranceService.policies);
        }

        [HttpPost("customer")]
        [ActionName("CreateCustomer")]
        public ActionResult<Customer> CreateCustomer(CreateCustomerRequest request) 
        {
            try
            {
                Customer customer = _service.CreateCustomer(request);
                return Ok(customer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("policy")]
        [ActionName("CreatePolicy")]
        public ActionResult<Policy> CreatePolicy(CreatePolicyRequest request)
        {
            try
            {
                Policy policy = _service.CreatePolicy(request);
                return Ok(policy);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
