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
            return Ok(PolicyService.policies);
        }

        [HttpGet("policy/{policyNumber}")]
        public ActionResult<Policy> GetPolicyByNumber(int policyNumber)
        {
            Policy? policy = PolicyService.FindPolicyByNumber(policyNumber);

            if (policy is null)
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
                return PolicyService.CreatePolicy(request);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

}
