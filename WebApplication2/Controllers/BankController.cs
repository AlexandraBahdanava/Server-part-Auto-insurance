using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication2.Models.Data;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly AvtoStrachovanieDbContext _context;

        public BanksController(AvtoStrachovanieDbContext context)
        {
            _context = context;
        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            var banks = await _context.Banks.ToListAsync();
            return Ok(banks); // 200 OK
        }

        // GET: api/Bank/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            var bank = await _context.Banks.FindAsync(id);

            if (bank == null)
            {
                return NotFound($"No banks with ID  '{id}'.");
            }

            return Ok(bank); // 200 OK
        }

        // GET: /api/Bank/search?name=Название_Банка
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBankByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("The 'name' parameter is required."); // 400 Bad Request
            }

            var banks = await _context.Banks.ToListAsync();
            var regex = new Regex(name, RegexOptions.IgnoreCase);
            var filteredBanks = banks.Where(b => regex.IsMatch(b.Name)).ToList();

            if (filteredBanks == null || !filteredBanks.Any())
            {
                return NotFound($"No banks found with the name '{name}'."); // 404 Not Found
            }

            return Ok(filteredBanks); // 200 OK
        }


        // POST: api/Bank
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            if (_context.Banks.Any(b => b.Id == bank.Id))
            {
                return Conflict($"A bank with ID {bank.Id} already exists."); // 409 Conflict
            }

            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBank), new { id = bank.Id }, bank); // 201 Created
        }


        // PUT: api/Bank/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(int id, Bank bank)
        {
            if (id != bank.Id)
            {
                return BadRequest(); // 400 Bad Request
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok($"Bank information has been successfully changed.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
                {
                    return NotFound(); // 404 Not Found
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 No Content
        }

        // DELETE: api/Bank/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound("No such bank exists"); // 404 Not Found
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();

            return Ok("Bank information successfully deleted"); // 204 No Content
        }

        private bool BankExists(int id)
        {
            return _context.Banks.Any(e => e.Id == id);
        }
    }
}
