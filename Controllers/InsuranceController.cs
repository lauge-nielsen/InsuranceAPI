using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController() : ControllerBase
    {
        [HttpGet("price")]
        [ActionName("GetPrice")]
        public ActionResult<double> GetPrice(string customerId, string insuranceType)
        {
            Customer? customer = InsuranceService.GetCustomerById(customerId);

            if (customer == null) 
            { 
                return BadRequest("Customer not found");
            }

            return Ok(InsuranceService.CalculatePrice(customer, insuranceType));
        }

        [HttpGet("customer")]
        [ActionName("GetCustomers")]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(InsuranceService.customers);
        }

        [HttpGet("quote")]
        [ActionName("GetQuotes")]
        public ActionResult<List<Quote>> GetQuotes()
        {
            return Ok(InsuranceService.quotes);
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
                Customer customer = InsuranceService.CreateCustomer(request);
                return Ok(customer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("quote")]
        [ActionName("CreateQuote")]
        public ActionResult<Quote> CreateQuote(CreateQuoteRequest request)
        {
            try
            {
                Quote quote = InsuranceService.CreateQuote(request);
                return Ok(quote);
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
                Policy policy = InsuranceService.CreatePolicy(request);
                return Ok(policy);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("quote/{quoteId}")]
        [ActionName("UpdateQuote")]
        public ActionResult<Quote> UpdateQuote(string quoteId, UpdateQuoteRequest request)
        {
            try
            {
                Quote quote = InsuranceService.UpdateQuote(quoteId, request);
                return Ok(quote);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("quote/{quoteId}")]
        [ActionName("DeleteQuote")]
        public ActionResult<Quote> DeleteQuote(string quoteId)
        {
            try
            {
                InsuranceService.DeleteQuote(quoteId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
