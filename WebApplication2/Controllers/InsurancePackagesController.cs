using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePackagesController : ControllerBase
    {
        private readonly AvtoStrachovanieDbContext _context;

        public InsurancePackagesController(AvtoStrachovanieDbContext context)
        {
            _context = context;
        }

        // GET: api/InsurancePackages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsurancePackages>>> GetInsurancePackages()
        {
            return await _context.InsurancePackages.ToListAsync();
        }

        // GET: api/InsurancePackages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsurancePackages>> GetInsurancePackage(int id)
        {
            var insurancePackage = await _context.InsurancePackages.FindAsync(id);

            if (insurancePackage == null)
            {
                return NotFound($"No insurance packages found with ID  '{id}'.");
            }

            return insurancePackage;
        }

        // GET: /api/InsurancePackages/searchByCarType?carType=carType
        [HttpGet("searchByCarType")]
        public async Task<ActionResult<IEnumerable<InsurancePackages>>> GetInsurancePackagesByCarType([FromQuery] string carType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(carType))
                {
                    return BadRequest("The 'carType' parameter is required."); // 400 Bad Request
                }

                var insurancePackages = await _context.InsurancePackages
                    .Where(ip => EF.Functions.Like(ip.carType, $"%{carType}%"))
                    .ToListAsync();

                if (insurancePackages == null || !insurancePackages.Any())
                {
                    return NotFound($"No insurance packages found for the car type '{carType}'."); // 404 Not Found
                }

                return Ok(insurancePackages); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception, you can also log it to a file or a monitoring system
                Console.WriteLine($"Error: {ex.Message}");

                return StatusCode(500, "An error occurred while processing the request."); // 500 Internal Server Error
            }
        }


        //GET: /api/InsurancePackages/searchByBankId?bankId=bankId
        [HttpGet("searchByBankId")]
        public async Task<ActionResult<IEnumerable<InsurancePackages>>> GetInsurancePackagesByBankId([FromQuery] int bankId)
        {
            var insurancePackages = await _context.InsurancePackages
                .Where(ip => ip.BankId == bankId)
                .ToListAsync();

            if (insurancePackages == null || !insurancePackages.Any())
            {
                return NotFound($"No insurance packages found for the bank with ID '{bankId}'."); // 404 Not Found
            }

            return Ok(insurancePackages); // 200 OK
        }


        [HttpPost]
        public async Task<ActionResult<InsurancePackages>> PostInsurancePackage(InsurancePackages insurancePackage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            if (_context.InsurancePackages.Any(ip => ip.Id == insurancePackage.Id))
            {
                return Conflict($"An insurance package with ID {insurancePackage.Id} already exists."); // 409 Conflict
            }

            _context.InsurancePackages.Add(insurancePackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInsurancePackage), new { id = insurancePackage.Id }, insurancePackage); // 201 Created
        }


        // PUT: api/InsurancePackages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurancePackage(int id, InsurancePackages insurancePackage)
        {
            if (id != insurancePackage.Id)
            {
                return BadRequest("Mismatched IDs"); // 400 Bad Request
            }

            _context.Entry(insurancePackage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsurancePackageExists(id))
                {
                    return NotFound($"Insurance package with ID {id} not found"); // 404 Not Found
                }
                else
                {
                    throw;
                }
            }

            return Ok(insurancePackage); // 200 OK
        }

        // DELETE: api/InsurancePackages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurancePackage(int id)
        {
            var insurancePackage = await _context.InsurancePackages.FindAsync(id);
            if (insurancePackage == null)
            {
                return NotFound($"Insurance package with ID {id} not found"); // 404 Not Found
            }

            _context.InsurancePackages.Remove(insurancePackage);
            await _context.SaveChangesAsync();

            return Ok(insurancePackage); // 200 OK
        }


        private bool InsurancePackageExists(int id)
        {
            return _context.InsurancePackages.Any(e => e.Id == id);
        }
    }
}
