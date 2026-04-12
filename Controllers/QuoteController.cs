using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    public class QuoteController() : ControllerBase
    {
        [HttpGet("quote")]
        public ActionResult<List<Quote>> GetQuotes()
        {
            return Ok(InsuranceService.quotes);
        }

        [HttpGet("quote/{quoteId}")]
        public ActionResult<Quote> GetQuoteById(string quoteId)
        {
            Quote? quote = InsuranceService.FindQuoteById(quoteId);

            if (quote == null)
            {
                return NotFound("Quote not found");
            }

            return Ok(quote);
        } 

        [HttpPost("quote")]
        public ActionResult<Quote> CreateQuote([FromBody] CreateQuoteRequest request)
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

        [HttpPut("quote/{quoteId}")]
        public ActionResult<Quote> UpdateQuote(string quoteId, [FromBody] UpdateQuoteRequest request)
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
