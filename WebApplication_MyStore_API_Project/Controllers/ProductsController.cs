using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_MyStore_API_Project.Data;
using WebApplication_MyStore_API_Project.DTO;
using WebApplication_MyStore_API_Project.Models;

namespace WebApplication_MyStore_API_Project.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IWebHostEnvironment _env;

        public ProductsController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }




        [HttpPost("Post-Products")]
        public async Task<ActionResult> CreateProducts([FromForm] ProductDTO pDTO)
        {
            string imagePath = string.Empty;

            if (pDTO.Image != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(pDTO.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pDTO.Image.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            var newProduct = new Product
            {
                Title = pDTO.Title,
                Category = pDTO.Category,
                Rating = pDTO.Rating,
                Price = pDTO.Price,
                ImageUrl = imagePath
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }




        //--------------------------------------------------------------------------------

        

        [AllowAnonymous] 

        [HttpGet("Get-All-Products")]

        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }



        [AllowAnonymous]

        [HttpGet("Get-Products-By-ID/{ProductID}")]

        public async Task<ActionResult> GetByID(int ProductID)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == ProductID);

            if (product == null)
            {
                return NotFound("Product Not Found");
            }

            return Ok(product);
        }



        [AllowAnonymous]

        [HttpGet("Get-Products-By-Category/{ProductCategory}")]

        public async Task<ActionResult> GetByCategory(string ProductCategory)
        {
            var products = await _context.Products.Where(p => p.Category.ToLower() == ProductCategory.ToLower()).ToListAsync();


            if (products == null)
            {
                return NotFound("No Products Found in this Category");
            }


            return Ok(products);

        }




        //--------------------------------------------------------------------------------



        [HttpDelete("{ProductID:int}")]
        public async Task<IActionResult> DeleteProduct(int ProductID)
        {
            var product = await _context.Products.FindAsync(ProductID);

            if (product == null)
            {
                return NotFound();
            }


            if (!string.IsNullOrWhiteSpace(product.ImageUrl))
            {
                var relativePath = product.ImageUrl.TrimStart('/', '\\');
                var imagePath = Path.Combine(_env.WebRootPath, relativePath);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }


            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }




        //--------------------------------------------------------------------------------






        [HttpPut("Update-Products/{ProductID}")]

        public async Task<ActionResult> UpdateProducts(int ProductID, [FromForm] ProductDTO pDTO)
        {
            var existingProduct = await _context.Products.FindAsync(ProductID);

            if (existingProduct == null)
            {
                return NotFound("Product Not Found");
            }

            if (pDTO.Image != null)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(pDTO.Image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await pDTO.Image.CopyToAsync(stream);
                }
                if (!string.IsNullOrWhiteSpace(existingProduct.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_env.WebRootPath, existingProduct.ImageUrl.TrimStart('/', '\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                existingProduct.ImageUrl = "/images/" + fileName;
            }

            existingProduct.Title = pDTO.Title;
            existingProduct.Category = pDTO.Category;
            existingProduct.Rating = pDTO.Rating;
            existingProduct.Price = pDTO.Price;
            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }











        //--------------------------------------------------------------------------------




        [HttpPatch("{ProductID}")]

        public async Task<IActionResult> UpdateProductPartially(int ProductID, [FromForm] ProductDTO pDTO)
        {

            var product = await _context.Products.FindAsync(ProductID);

            if (product == null)
            {
                return NotFound();
            }


            if (!string.IsNullOrWhiteSpace(pDTO.Title))
            {
                product.Title = pDTO.Title;
            }


            if (!string.IsNullOrWhiteSpace(pDTO.Category))
            {
                product.Category = pDTO.Category;
            }


            if (pDTO.Rating != 0)
            {
                product.Rating = pDTO.Rating;
            }


            if (pDTO.Price != 0)
            {
                product.Price = pDTO.Price;
            }

            await _context.SaveChangesAsync();
            return Ok(product);
        }
    }
}




