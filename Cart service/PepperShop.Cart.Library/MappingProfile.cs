using AutoMapper;

namespace PepperShop.Cart.Library
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Entities.Cart, Dtos.Cart>()
                .ForSourceMember(_ => _.CreatedAt, opt => opt.DoNotValidate());
            CreateMap<Data.Entities.CartItem, Dtos.CartItem>();
        }
    }
}
