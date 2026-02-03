using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication_MyStore_API_Project.Data;
using WebApplication_MyStore_API_Project.DTO;
using WebApplication_MyStore_API_Project.Models;

namespace WebApplication_MyStore_API_Project.Controllers
{
    [Route("Cart/[controller]")]
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly DataContext _context;


        public CartController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Get-Cart")]
        public async Task<ActionResult> GetCart()
        {
            var cartItems = await _context.CartItems.ToListAsync();
            return Ok(cartItems);
        }




        [HttpGet("Get-Cart-Item-By-Id/{CartID}")]

        public async Task<ActionResult> GetCartItemById(int CartID)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == CartID);

            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }
            return Ok(cartItem);
        }

        //------------------------------------




        [HttpDelete("DeleteCart/{CartID}")]

        public async Task<ActionResult> DeleteFromCart(int CartID)
        {
            var CartProducts = _context.CartItems.FirstOrDefault(p => p.Id == CartID);

            if (CartProducts == null)
            {
                return NotFound();
            }


            _context.CartItems.Remove(CartProducts);

            await _context.SaveChangesAsync();

            return Ok(CartProducts);
        }
        //------------------------------------







        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddToCart([FromBody] CartDTO dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == dto.ProductId);

            if (existingCartItem != null)
            {
                //existingCartItem.Quantity += dto.Quantity;


                existingCartItem.Quantity += dto.Quantity;
                existingCartItem.Title = product.Title;
                existingCartItem.Price = product.Price;
                existingCartItem.ImageUrl = product.ImageUrl;


            }

            else
            {
                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = dto.Quantity
                };

                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            return Ok("Product added to cart successfully");
        }


        //---------------------------------------------------------------------------------


        [HttpPatch("Update-Products/{CartID}")]

        public async Task<ActionResult> UpdateCartItems(int CartID, [FromBody] CartDTO dto)
        {
            var cartItem = _context.CartItems.FirstOrDefault(p => p.Id == CartID);

            if (cartItem == null)
            {
                return NotFound("Cart item not found");
            }

            cartItem.Quantity = dto.Quantity;

            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }
    }

}
