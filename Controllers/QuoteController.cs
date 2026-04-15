using InsuranceAPI.DTOs.Requests;
using InsuranceAPI.DTOs.Responses;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers
{
    [ApiController]
    [Tags("Quotes")]
    public class QuoteController() : ControllerBase
    {
        [HttpGet("quote")]
        public ActionResult<List<Quote>> GetQuotes()
        {
            return Ok(QuoteService.quotes);
        }

        [HttpGet("quote/{quoteId}")]
        public ActionResult<Quote> GetQuoteById(string quoteId)
        {
            Quote? quote = QuoteService.FindQuoteById(quoteId);

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
                Quote quote = QuoteService.CreateQuote(request);
                QuoteResponse response = new(quote);
                return CreatedAtAction(nameof(GetQuoteById), new { quoteId = quote.QuoteId }, response);
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
                Quote quote = QuoteService.UpdateQuote(quoteId, request);
                QuoteResponse response = new(quote);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("quote/{quoteId}/accept")]
        public ActionResult<Quote> AcceptQuote(string quoteId)
        {
            try
            {
                Quote quote = QuoteService.AcceptQuote(quoteId);
                QuoteResponse response = new(quote);
                return Ok(response);
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
                QuoteService.DeleteQuote(quoteId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

}
