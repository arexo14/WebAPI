using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nauka.Database;
using nauka.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nauka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;

        public ProductController(ProductDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public List<Entities.Product> Get()
        {
            var result = _dbContext.Products!.ToList();
            return result;
        }


        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (_dbContext == null)
            {
                return StatusCode(500, "Internal server error: Database context is null");
            }

            if (product == null)
            {
                return BadRequest("Product object is null");
            }

            try
            {
                if (_dbContext.Products != null)
                {
                    _dbContext.Products.Add(product);
                    _dbContext.SaveChanges();
                    return Ok(product);
                }
                else
                {
                    return StatusCode(500, "Internal server error: Products collection is null");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }




        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product updatedProduct)
        {
            if (_dbContext == null)
            {
                return StatusCode(500, "Internal server error: Database context is null");
            }

            if (updatedProduct == null)
            {
                return BadRequest("Product object is null");
            }

            try
            {
                var existingProduct = _dbContext.Products!.Find(id);

                if (existingProduct != null)
                {
                    existingProduct.Name = updatedProduct.Name; 
                    existingProduct.Price = updatedProduct.Price; 

                    _dbContext.SaveChanges(); 
                    return Ok(existingProduct);
                }
                else
                {
                    return NotFound("Product not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_dbContext == null)
            {
                return StatusCode(500, "Internal server error: Database context is null");
            }

            try
            {
                var existingProduct = _dbContext.Products!.Find(id); 

                if (existingProduct != null)
                {
                    _dbContext.Products.Remove(existingProduct); 
                    _dbContext.SaveChanges();

                    return Ok("Product deleted successfully");
                }
                else
                {
                    return NotFound("Product not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

