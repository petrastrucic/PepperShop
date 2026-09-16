using AutoMapper;
using PepperShop.Cart.Data.Repositories;

namespace PepperShop.Cart.Library.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository<Data.Entities.Cart> _repository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository<Data.Entities.Cart> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Dtos.Cart> GetCartAsync(string userId)
        {
            var cart = await _repository.GetAsync(userId);
            return _mapper.Map<Dtos.Cart>(cart);
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
