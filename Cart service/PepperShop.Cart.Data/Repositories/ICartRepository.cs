namespace PepperShop.Cart.Data.Repositories
{
    public interface ICartRepository<T>
    {
        Task<T> GetAsync(string cartId);
        Task<T> GetDetailsAsync(string cartId);
        Task<T> AddProductToCartAsync(string cartId, string productId);
        Task<T> RemoveProductFromCartAsync(string cartId, string productId);
        Task<T> UpdateProductQuantityInCartAsync(string cartId, string productId, int newQuantity);
    }
}
