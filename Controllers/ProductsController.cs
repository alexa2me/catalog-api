using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {

            try
            {
                const int maxPageSize = 35;
                if (pageNumber <= 0) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;
                if (pageSize > maxPageSize) pageSize = maxPageSize;

                var productsQuery = _context.Products.AsNoTracking();
                var products = await productsQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(products);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            try
            {
                var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

                if (product is null)
                {
                    return NotFound($"Product with ID {id} not found. Sorry.");
                }
                return product;
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostAsync([FromBody] Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest("Oh no. Invalid product data.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return new CreatedAtRouteResult("GetProduct",
                    new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> PutAsync(int id, [FromBody] Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest("Oh no. Invalid product data.");
                }

                if (id != product.Id)
                {
                    return BadRequest("Oh no. Product ID mismatch.");
                }

                bool exists = _context.Products.Any(p => p.Id == id);
                if (!exists)
                {
                    return NotFound($"Product with ID {id} not found. Sorry");
                }

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteAsync(int id)
        {
            try
            {
                var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

                if (product is null)
                {
                    return NotFound($"Product with ID {id} not found. Sorry.");
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception)
            {
                return ErrorResultHelper.InternalServerErrorResult(this);
            }
        }
    }
}