using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController() : ControllerBase
    {
        [HttpGet("price")]
        public ActionResult<double> GetPrice(string customerId, string insuranceType)
        {
            try
            {
                Customer customer = InsuranceService.GetCustomerById(customerId);
                return Ok(InsuranceService.CalculatePrice(customer, insuranceType));
            }
            catch (ArgumentException)
            {
                return NotFound("Customer not found");
            }
        }

        [HttpGet("customer")]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(InsuranceService.customers);
        }

        [HttpGet("quote")]
        public ActionResult<List<Quote>> GetQuotes()
        {
            return Ok(InsuranceService.quotes);
        }

        [HttpGet("quote/{quoteId}")]
        public ActionResult<Quote> GetQuoteById(string quoteId)
        {
            try
            {
                Quote quote = InsuranceService.GetQuoteById(quoteId);
                return Ok(quote);
            }
            catch (ArgumentException)
            {
                return NotFound("Quote not found");
            }
            
        }

        [HttpGet("policy")]
        public ActionResult<List<Policy>> GetPolicies()
        {
            return Ok(InsuranceService.policies);
        }

        [HttpGet("policy/{policyNumber}")]
        public ActionResult<Policy> GetPolicyByNumber(int policyNumber)
        {
            try
            {
                Policy policy = InsuranceService.GetPolicyByNumber(policyNumber);
                return Ok(policy);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("customer")]
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
        public ActionResult<Quote> CreateQuote(CreateQuoteRequest request)
        {
            try
            {
                Quote quote = InsuranceService.CreateQuote(request);
                return CreatedAtAction(nameof(GetQuoteById), new { quoteId = quote.QuoteId }, quote);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("policy")]
        public ActionResult<Policy> CreatePolicy(CreatePolicyRequest request)
        {
            try
            {
                Policy policy = InsuranceService.CreatePolicy(request);
                return CreatedAtAction(nameof(GetPolicyByNumber), new { policyNumber = policy.PolicyNumber }, policy);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("quote/{quoteId}")]
        public ActionResult<Quote> UpdateQuote(string quoteId, UpdateQuoteRequest request)
        {
            try
            {
                Quote quote = InsuranceService.UpdateQuote(quoteId, request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("quote/{quoteId}")]
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
