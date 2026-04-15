using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Models.Insurances;
using InsuranceAPI.Services;
using InsuranceAPI.Services.Factories;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    public class PricingController() : ControllerBase
    {
        [HttpGet("price")]
        public ActionResult<double> GetPrice(string customerId, InsuranceRequest insuranceRequest)
        {
            Customer? customer = CustomerService.FindCustomerById(customerId);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            Insurance? insurance = InsuranceFactory.Create(insuranceRequest);
            return Ok(PricingService.CalculatePrice(customer, insurance));
        }

    }

}
