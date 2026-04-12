using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    public class PolicyController() : ControllerBase
    {
        [HttpGet("policy")]
        public ActionResult<List<Policy>> GetPolicies()
        {
            return Ok(InsuranceService.policies);
        }

        [HttpGet("policy/{policyNumber}")]
        public ActionResult<Policy> GetPolicyByNumber(int policyNumber)
        {
            Policy? policy = InsuranceService.GetPolicyByNumber(policyNumber);

            if (policy == null)
            {
                return NotFound("Policy not found");
            }

            return Ok(policy);
        }

        [HttpPost("policy")]
        public ActionResult<Policy> CreatePolicy([FromBody] CreatePolicyRequest request)
        {
            try
            {
                return InsuranceService.CreatePolicy(request);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
