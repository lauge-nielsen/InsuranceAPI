using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    public class PricingController() : ControllerBase
    {
        [HttpGet("price")]
        public ActionResult<double> GetPrice(string customerId, InsuranceType insuranceType)
        {
            Customer? customer = CustomerService.FindCustomerById(customerId);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(PricingService.CalculatePrice(customer, insuranceType));
        }

    }

}
