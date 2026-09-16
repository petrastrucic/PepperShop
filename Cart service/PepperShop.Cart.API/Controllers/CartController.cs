using Microsoft.AspNetCore.Mvc;
using PepperShop.Cart.Library.Services;

namespace PepperShop.Cart.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAsync(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    _logger.LogWarning($"Invalid user ID: {userId}");
                    return BadRequest(new { message = "User ID must be a non-empty string." });
                }

                var cart = await _cartService.GetCartAsync(userId);

                _logger.LogInformation($"Cart retrieved successfully for user: {userId}");
                return Ok(cart);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"Invalid argument while retrieving cart for user {userId}.");
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Business rule violation while fetching cart {userId}.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error while retrieving cart for user {userId}.");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("{userId}/details")]
        public async Task<Library.Dtos.Cart> GetDetailsAsync(string userId)
        {
            return await _cartService.GetCartDetailsAsync(userId);
        }

        [HttpPatch("{userId}/addProduct")]
        public async Task<Library.Dtos.Cart> AddProductAsync(string userId, string productId)
        {
            return await _cartService.AddProductToCartAsync(userId, productId);
        }

        [HttpPatch("{userId}/removeProduct")]
        public async Task<Library.Dtos.Cart> RemoveProductAsync(string userId, string productId)
        {
            return await _cartService.RemoveProductFromCartAsync(userId, productId);
        }

        [HttpPatch("{userId}/updateQuantity")]
        public async Task<Library.Dtos.Cart> UpdateQuantityAsync(string userId, string productId, int newQuantity)
        {
            return await _cartService.UpdateProductQuantityInCartAsync(userId, productId, newQuantity);
        }
    }
}
