using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementsController : ControllerBase
    {
        private readonly AvtoStrachovanieDbContext _context;

        public StatementsController(AvtoStrachovanieDbContext context)
        {
            _context = context;
        }

        // GET: api/Statements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Statements>>> GetStatements()
        {
            return await _context.Statements.ToListAsync();
        }

        // GET: api/Statements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Statements>> GetStatement(int id)
        {
            var statement = await _context.Statements.FindAsync(id);

            if (statement == null)
            {
                return NotFound($"No statements  with ID '{id}'."); // 404 Not Found
            }

            return statement;
        }

        // GET: /api/Statements/searchByUserId?userId=userId

        [HttpGet("searchByUserId")]
        public async Task<ActionResult<IEnumerable<Statements>>> GetStatementsByUserId([FromQuery] int userId)
        {
            var statements = await _context.Statements
                .Where(s => s.UserId == userId)
                .ToListAsync();

            if (statements == null || !statements.Any())
            {
                return NotFound($"No statements found for the user with ID '{userId}'."); // 404 Not Found
            }

            return Ok(statements); // 200 OK
        }


        // POST: api/Statements
        [HttpPost]
        public async Task<ActionResult<Statements>> PostStatement(Statements statement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            if (_context.Statements.Any(s => s.Id == statement.Id))
            {
                return Conflict($"A statement with ID {statement.Id} already exists."); // 409 Conflict
            }

            _context.Statements.Add(statement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatement), new { id = statement.Id }, statement); // 201 Created
        }


        //// PUT: api/Statements/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStatement(int id, Statements statement)
        //{
        //    if (id != statement.Id)
        //    {
        //        return BadRequest("Mismatched IDs"); // 400 Bad Request
        //    }

        //    _context.Entry(statement).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StatementExists(id))
        //        {
        //            return NotFound($"Statement with ID {id} not found"); // 404 Not Found
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(statement); // 200 OK
        //}

        // DELETE: api/Statements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatement(int id)
        {
            var statement = await _context.Statements.FindAsync(id);
            if (statement == null)
            {
                return NotFound($"Statement with ID {id} not found"); // 404 Not Found
            }

            _context.Statements.Remove(statement);
            await _context.SaveChangesAsync();

            return Ok(statement); // 200 OK
        }


        private bool StatementExists(int id)
        {
            return _context.Statements.Any(e => e.Id == id);
        }
    }
}
