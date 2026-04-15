using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.DTOs.Responses;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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
                Policy policy = PolicyService.CreatePolicy(request);
                PolicyResponse response = new(policy);
                return CreatedAtAction(nameof(GetPolicyByNumber), new { policyNumber = policy.PolicyNumber }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

}
