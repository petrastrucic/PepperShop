using Microsoft.Azure.Cosmos;

namespace PepperShop.Cart.Data.Repositories
{
    public class CartRepository : ICartRepository<Entities.Cart>
    {
        private readonly Container _container;

        public CartRepository(CosmosClient client, string databaseName, string containerName)
        {
            _container = client.GetContainer(databaseName, containerName);
        }

        #region 
        public async Task<Entities.Cart> GetAsync(string cartId)
        {
            try
            {
                var response = await _container.ReadItemAsync<Entities.Cart>(cartId, new PartitionKey(cartId));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public Task<Entities.Cart> GetDetailsAsync(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.Cart> AddProductToCartAsync(string cartId, string productId)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.Cart> RemoveProductFromCartAsync(string cartId, string productId)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.Cart> UpdateProductQuantityInCartAsync(string cartId, string productId, int newQuantity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
