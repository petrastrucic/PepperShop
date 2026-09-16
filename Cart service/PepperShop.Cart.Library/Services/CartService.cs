using AutoMapper;
using Microsoft.Extensions.Logging;
using PepperShop.Cart.Data.Repositories;

namespace PepperShop.Cart.Library.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository<Data.Entities.Cart> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository<Data.Entities.Cart> repository, IMapper mapper, ILogger<CartService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Dtos.Cart> GetCartAsync(string userId)
        {
            try
            {
                _logger.LogInformation($"Fetching cart with id: {userId}...");
                var cart = await _repository.GetAsync(userId);

                if (cart == null)
                {
                    _logger.LogWarning($"Cart with id {userId} not found.");
                    throw new InvalidOperationException($"Cart {userId} not found.");
                }

                _logger.LogInformation($"Cart for user {userId} fetched successfully.");

                return _mapper.Map<Dtos.Cart>(cart);
            }
            catch (InvalidOperationException ex)
            {
                // Known business exception
                _logger.LogWarning(ex, $"Business rule violation while fetching cart {userId}.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error while fetching cart {userId}.");
                throw;
            }
        }

        public async Task<Dtos.Cart> GetCartDetailsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Dtos.Cart> AddProductToCartAsync(string userId, string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Dtos.Cart> RemoveProductFromCartAsync(string userId, string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Dtos.Cart> UpdateProductQuantityInCartAsync(string userId, string productId, int newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
