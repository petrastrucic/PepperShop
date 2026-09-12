using AutoMapper;

namespace PepperShop.Cart.Library
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Entities.Cart, Dtos.Cart>();
            CreateMap<Data.Entities.CartItem, Dtos.CartItem>();
        }
    }
}
