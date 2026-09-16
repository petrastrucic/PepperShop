using Microsoft.AspNetCore.Mvc;
using PepperShop.Cart.Library.Services;

namespace PepperShop.Cart.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("/")]
        public async Task<Library.Dtos.Cart> GetAsync(string userId)
        {
            return await _cartService.GetCartAsync(userId);
        }

        [HttpGet("/details")]
        public async Task<Library.Dtos.Cart> GetDetailsAsync(string userId)
        {
            return await _cartService.GetCartDetailsAsync(userId);
        }

        [HttpPatch("/addProduct")]
        public async Task<Library.Dtos.Cart> AddProductAsync(string userId, string productId)
        {
            return await _cartService.AddProductToCartAsync(userId, productId);
        }

        [HttpPatch("/removeProduct")]
        public async Task<Library.Dtos.Cart> RemoveProductAsync(string userId, string productId)
        {
            return await _cartService.RemoveProductFromCartAsync(userId, productId);
        }

        [HttpPatch("/updateQuantity")]
        public async Task<Library.Dtos.Cart> UpdateQuantityAsync(string userId, string productId, int newQuantity)
        {
            return await _cartService.UpdateProductQuantityInCartAsync(userId, productId, newQuantity);
        }
    }
}
