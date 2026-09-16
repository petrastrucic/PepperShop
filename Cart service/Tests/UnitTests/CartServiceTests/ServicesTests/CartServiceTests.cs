using AutoMapper;
using Moq;
using PepperShop.Cart.Data.Repositories;
using PepperShop.Cart.Library.Dtos;
using PepperShop.Cart.Library.Services;

namespace CartServiceTests.ServicesTests
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<ICartRepository<PepperShop.Cart.Data.Entities.Cart>> _cartRepositoryMock = null!;
        private CartService _cartService = null!;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>(MockBehavior.Default);
            _mapperMock
                .Setup(m => m.Map<Cart>(It.IsAny<PepperShop.Cart.Data.Entities.Cart>()))
                .Returns((PepperShop.Cart.Data.Entities.Cart src) => new Cart
                {
                    Id = src.id,
                    Items = null,   // TODO: Map items if needed
                    Discount = src.Discount,
                    TotalPrice = src.TotalPrice,
                    Currency = src.Currency
                });
            _cartRepositoryMock = new Mock<ICartRepository<PepperShop.Cart.Data.Entities.Cart>>();
            _cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetCartAsync_ValidEntity_ReturnsMappedDto()
        {
            // Arrange
            var entity = new PepperShop.Cart.Data.Entities.Cart()
            {
                id = "1",
                Items = null,
                Discount = 0.0m,
                TotalPrice = 0.0m,
                Currency = "USD"
            };
            _cartRepositoryMock
               .Setup(r => r.GetAsync(entity.id))
               .ReturnsAsync(entity);

            // Act
            var dto = await _cartService.GetCartAsync(entity.id);

            // Assert
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Id, Is.EqualTo("1"));
            Assert.That(dto.Items, Is.Null);
            Assert.That(dto.Discount, Is.EqualTo(0.0m));
            Assert.That(dto.TotalPrice, Is.EqualTo(0.0m));
            Assert.That(dto.Currency, Is.EqualTo("USD"));

            // Verify mapper was called exactly once
            _mapperMock.Verify(m => m.Map<Cart>(entity), Times.Once);
        }
    }
}
