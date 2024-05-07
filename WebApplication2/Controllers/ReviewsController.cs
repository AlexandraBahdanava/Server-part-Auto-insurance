using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly AvtoStrachovanieDbContext _context;

        public ReviewsController(AvtoStrachovanieDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound($"No reviews found  with ID '{id}'.");
            }

            return review;
        }

        // GET: /api/Reviews/searchByBankId?bankId=BankId
        [HttpGet("searchByBankId")]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviewsByBankId([FromQuery] int bankId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.BankId == bankId)
                .ToListAsync();

            if (reviews == null || !reviews.Any())
            {
                return NotFound($"No reviews found for the bank with ID '{bankId}'."); // 404 Not Found
            }

            return Ok(reviews); // 200 OK
        }

        //GET: /api/Reviews/searchByUserId?userId=UserId
        [HttpGet("searchByUserId")]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviewsByUserId([FromQuery] int userId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.UserId == userId)
                .ToListAsync();

            if (reviews == null || !reviews.Any())
            {
                return NotFound($"No reviews found for the user with ID '{userId}'."); // 404 Not Found
            }

            return Ok(reviews); // 200 OK
        }


        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReview(Reviews review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            if (_context.Reviews.Any(r => r.Id == review.Id))
            {
                return Conflict($"A review with ID {review.Id} already exists."); // 409 Conflict
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review); // 201 Created
        }

        //// PUT: api/Reviews/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutReview(int id, Reviews review)
        //{
        //    if (id != review.Id)
        //    {
        //        return BadRequest("Mismatched IDs"); // 400 Bad Request
        //    }

        //    _context.Entry(review).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ReviewExists(id))
        //        {
        //            return NotFound($"Review with ID {id} not found"); // 404 Not Found
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(review); // 200 OK
        //}

        //// DELETE: api/Reviews/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteReview(int id)
        //{
        //    var review = await _context.Reviews.FindAsync(id);
        //    if (review == null)
        //    {
        //        return NotFound($"Review with ID {id} not found"); // 404 Not Found
        //    }

        //    _context.Reviews.Remove(review);
        //    await _context.SaveChangesAsync();

        //    return Ok(review); // 200 OK
        //}


        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
