using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models.Data;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly AvtoStrachovanieDbContext _context;

        public CarsController(AvtoStrachovanieDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cars>>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return Ok(cars); // 200 OK
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cars>> GetCars(int id)
        {
            var cars = await _context.Cars.FindAsync(id);

            if (cars == null)
            {
                return NotFound($"No cars found with ID  '{id}'.");
            }

            return Ok(cars); // 200 OK
        }

        // GET: /api/Cars/searchByNumber?number=number
        [HttpGet("searchByNumber")]
        public async Task<ActionResult<IEnumerable<Cars>>> GetCarsByNumber([FromQuery] string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return BadRequest("The 'number' parameter is required."); // 400 Bad Request
            }

            var cars = await _context.Cars
                .Where(c => EF.Functions.Like(c.number, $"%{number}%"))
                .ToListAsync();

            if (cars == null || !cars.Any())
            {
                return NotFound($"No cars found with the number '{number}'."); // 404 Not Found
            }

            return Ok(cars); // 200 OK
        }



        //GET: /api/Cars/searchByPower?power=engineCapacity
        [HttpGet("searchByPower")]
        public async Task<ActionResult<IEnumerable<Cars>>> GetCarsByPower([FromQuery] string power)
        {
            var cars = await _context.Cars
                .Where(c => c.engineCapacity == power)
                .ToListAsync();

            if (cars == null || !cars.Any())
            {
                return NotFound($"No cars found with the engine power '{power}'."); // 404 Not Found
            }

            return Ok(cars); // 200 OK
        }


        // GET: /api/Cars/searchByUser?userId=UserId
        [HttpGet("searchByUser")]
        public async Task<ActionResult<IEnumerable<Cars>>> GetCarsByUser([FromQuery] int userId)
        {
            var cars = await _context.Cars
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cars == null || !cars.Any())
            {
                return NotFound($"No cars found for the user with ID '{userId}'."); // 404 Not Found
            }

            return Ok(cars); // 200 OK
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Cars>> PostCars(Cars cars)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            if (_context.Cars.Any(c => c.Id == cars.Id))
            {
                return Conflict($"A car with ID {cars.Id} already exists."); // 409 Conflict
            }

            _context.Cars.Add(cars);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCars), new { id = cars.Id }, cars); // 201 Created
        }


        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCars(int id, Cars cars)
        {
            if (id != cars.Id)
            {
                return BadRequest(); // 400 Bad Request
            }

            _context.Entry(cars).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarsExists(id))
                {
                    return NotFound(); // 404 Not Found
                }
                else
                {
                    throw;
                }
            }

            return Ok("Vehicle information successfully changed."); // 204 No Content
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCars(int id)
        {
            var cars = await _context.Cars.FindAsync(id);
            if (cars == null)
            {
                return NotFound("No such car exists"); // 404 Not Found
            }

            _context.Cars.Remove(cars);
            await _context.SaveChangesAsync();

            return Ok("Vehicle information successfully deleted."); // 204 No Content
        }

        private bool CarsExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
