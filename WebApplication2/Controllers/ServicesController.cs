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
    public class ServicesController : ControllerBase
    {
        private readonly AvtoStrachovanieDbContext _context;

        public ServicesController(AvtoStrachovanieDbContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Services>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound($"No services found  with ID '{id}'."); // 404 Not Found return NotFound();
            }

            return service;
        }

        // GET: /api/Services/searchByPackageId?packageId=InsurancePackagesId
        [HttpGet("searchByPackageId")]
        public async Task<ActionResult<IEnumerable<Services>>> GetServicesByPackageId([FromQuery] int packageId)
        {
            var services = await _context.Services
                .Where(s => s.InsurancePackagesId == packageId)
                .ToListAsync();

            if (services == null || !services.Any())
            {
                return NotFound($"No services found for the package with ID '{packageId}'."); // 404 Not Found
            }

            return Ok(services); // 200 OK
        }


        // POST: api/Services
        [HttpPost]
        public async Task<ActionResult<Services>> PostService(Services service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            if (_context.Services.Any(s => s.Id == service.Id))
            {
                return Conflict($"A service with ID {service.Id} already exists."); // 409 Conflict
            }

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.Id }, service); // 201 Created
        }


        // PUT: api/Services/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Services service)
        {
            if (id != service.Id)
            {
                return BadRequest("Mismatched IDs"); // 400 Bad Request
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound($"Service with ID {id} not found"); // 404 Not Found
                }
                else
                {
                    throw;
                }
            }

            return Ok(service); // 200 OK
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound($"Service with ID {id} not found"); // 404 Not Found
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(service); // 200 OK
        }


        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
