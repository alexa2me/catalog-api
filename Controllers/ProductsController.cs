using APICatalog.Context;
using APICatalog.Models;

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
        public ActionResult<IEnumerable<Product>> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            const int maxPageSize = 35;
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;
            if (pageSize > maxPageSize) pageSize = maxPageSize;

            var productsQuery = _context.Products.AsQueryable();
            var products = productsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                return NotFound("Product not found...");
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (product is null)
            {
                return BadRequest("Invalid product data.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product); // Include the new product in the context
            _context.SaveChanges(); // Save changes to the database

            return new CreatedAtRouteResult("GetProduct",
                new { id = product.Id }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            if (product is null)
            {
                return BadRequest("Invalid product data.");
            }

            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            bool exists = _context.Products.Any(p => p.Id == id);
            if (!exists)
            {
                return NotFound("Product not found.");
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found...");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}