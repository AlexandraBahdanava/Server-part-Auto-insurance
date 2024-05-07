using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Data;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthoriationDataController : ControllerBase
    {
        private readonly AvtoStrachovanieDbContext _context;

        public AuthoriationDataController(AvtoStrachovanieDbContext context)
        {
            _context = context;
        }

        // GET: api/AuthoriationData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthoriationData>>> GetAuthoriationData()
        {
            var authoriationDataList = await _context.AuthoriationData.ToListAsync();
            if (authoriationDataList == null || authoriationDataList.Count == 0)
            {
                return NotFound("No authoriation data found.");
            }

            return Ok(authoriationDataList);
        }

        // GET: api/AuthoriationData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthoriationData>> GetAuthoriationData(int id)
        {
            var authoriationData = await _context.AuthoriationData.FindAsync(id);

            if (authoriationData == null)
            {
                return NotFound($"Authoriation data with ID {id} not found.");
            }

            return Ok(authoriationData);
        }

        // POST: api/AuthoriationData
        [HttpPost]
        public async Task<ActionResult<AuthoriationData>> PostAuthoriationData(AuthoriationData authoriationData)
        {
            // Валидация данных
            var validationContext = new ValidationContext(authoriationData, null, null);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(authoriationData, validationContext, validationResults, true))
            {
                return BadRequest(validationResults);
            }

            _context.AuthoriationData.Add(authoriationData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthoriationData), new { id = authoriationData.Id }, authoriationData);
        }

        // PUT: api/AuthoriationData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthoriationData(int id, AuthoriationData authoriationData)
        {
            if (id != authoriationData.Id)
            {
                return BadRequest("ID mismatch.");
            }

            // Валидация данных
            var validationContext = new ValidationContext(authoriationData, null, null);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(authoriationData, validationContext, validationResults, true))
            {
                return BadRequest(validationResults);
            }

            _context.Entry(authoriationData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthoriationDataExists(id))
                {
                    return NotFound($"Authoriation data with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/AuthoriationData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthoriationData(int id)
        {
            var authoriationData = await _context.AuthoriationData.FindAsync(id);
            if (authoriationData == null)
            {
                return NotFound($"Authoriation data with ID {id} not found.");
            }

            _context.AuthoriationData.Remove(authoriationData);
            await _context.SaveChangesAsync();

            return Ok($"Authoriation data with ID {id} has been successfully deleted.");

        }

        private bool AuthoriationDataExists(int id)
        {
            return _context.AuthoriationData.Any(e => e.Id == id);
        }
    }
}
