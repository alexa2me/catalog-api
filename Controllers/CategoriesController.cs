using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Category>>> GetProductsCategoriesAsync()
        {
            try
            {
                return await _context.Categories
                .Include(p => p.Products)
                .AsNoTracking()
                .ToListAsync();
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                const int maxPageSize = 35;
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;
                if (pageSize > maxPageSize) pageSize = maxPageSize;

                var categoriesQuery = _context.Categories.AsNoTracking();
                var categories = await categoriesQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(categories);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<Category>> GetAsync(int id)
        {
            try
            {
                var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

                if (category is null)
                {
                    return NotFound($"Category with ID {id} not found. Sorry.");
                }

                return Ok(category);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostAsync([FromBody] Category category)
        {
            try
            {
                if (category is null)
                {
                    return BadRequest(
                        "Oh no. Invalid category data provided."
                    );
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.Id }, category);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> PutAsync(int id, [FromBody] Category category)
        {
            try
            {
                if (category is null)
                {
                    return BadRequest("Oh no. Invalid category data provided.");
                }

                if (id != category.Id)
                {
                    return BadRequest("Oh no. Category ID mismatch.");
                }

                bool exists = _context.Categories.Any(p => p.Id == id);
                if (!exists)
                {
                    return NotFound($"Category with ID {id} not found. Sorry");
                }

                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(category);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteAsync(int id)
        {
            try
            {
                var category = await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == id);

                if (category is null)
                {
                    return NotFound($"Category with ID {id} not found. Sorry.");
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(category);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }
    }
}