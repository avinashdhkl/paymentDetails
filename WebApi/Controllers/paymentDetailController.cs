using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class paymentDetailController : ControllerBase
    {
        private readonly DataContext dc;

        public paymentDetailController(DataContext dc)
        {
            this.dc = dc;
        }

        // GET: api/<paymentDetailController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<paymentDetails>>>  GetPaymentDetails()
        {
            return await dc.paymentDetails.ToListAsync();
        }

        // GET api/<paymentDetailController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<paymentDetails>> GetpaymentDetailsById(int id)
        {
            var paymentdetailsID = await dc.paymentDetails.FindAsync(id);
            if (paymentdetailsID == null)
            {
                return NotFound();
            };
            return paymentdetailsID;
        }

        // POST api/<paymentDetailController>
        [HttpPost]
        public async Task <ActionResult<paymentDetails>> AddPaymentDetails ([FromBody] paymentDetails paymentDetail )
        {
           dc.paymentDetails.Add(paymentDetail);

            await dc.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetails", new { id = paymentDetail.paymentDetailsID }, paymentDetail);
            
        }

        // PUT api/<paymentDetailController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentDetails(int id, [FromBody] paymentDetails paymentDetail)
        {
            if (id !=paymentDetail.paymentDetailsID)
            {
                return BadRequest("Invalid ID");

            }
            dc.Entry(paymentDetail).State = EntityState.Modified;
            try
            {
                await dc.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (!PaymentDetailsExists(id))
                {
                    return NotFound();

                }
                else
                {
                    throw;
                }
               
               
            }
            return Ok(200);
        }

        private bool PaymentDetailsExists(int id)
        {
            return dc.paymentDetails.Any(e => e.paymentDetailsID == id);
        }

        // DELETE api/<paymentDetailController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult>  DeletePaymentDetails(int id)

        {
            var paymentDtailsID = await dc.paymentDetails.FindAsync(id);
            if(paymentDtailsID == null)
            {
               return BadRequest("Invaild ID");
            }
            dc.paymentDetails.Remove(paymentDtailsID);
            await dc.SaveChangesAsync();
            return Ok(paymentDtailsID);
        }
    }
}
