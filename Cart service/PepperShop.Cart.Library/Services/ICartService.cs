namespace PepperShop.Cart.Library.Services
{
    public interface ICartService
    {
        Task<Dtos.Cart> GetCartAsync(string userId);

        Task<Dtos.Cart> GetCartDetailsAsync(string userId);

        Task<Dtos.Cart> AddProductToCartAsync(string userId, string productId);

        Task<Dtos.Cart> RemoveProductFromCartAsync(string userId, string productId);

        Task<Dtos.Cart> UpdateProductQuantityInCartAsync(string userId, string productId, int newQuantity);
    }
}
