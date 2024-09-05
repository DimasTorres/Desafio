using AutoMapper;
using Desafio.Core.Application.Contracts.Order.Request;
using Desafio.Core.Application.Contracts.Order.Response;
using Desafio.Core.Application.Contracts.Product.Request;
using Desafio.Core.Application.Contracts.Product.Response;
using Desafio.Core.Application.Contracts.User.Request;
using Desafio.Core.Application.Contracts.User.Response;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Mapper;

public class MapperFactory : Profile
{
    public MapperFactory()
    {
        OrderMap();
        ProductMap();
        UserMap();
    }

    private void OrderMap()
    {
        CreateMap<OrderEntity, OrderResponse>();
        CreateMap<CreateOrderRequest, OrderEntity>();
        CreateMap<OrderItemEntity, OrderItemResponse>();
        CreateMap<CreateOrderItemRequest, OrderItemEntity>();
    }
    private void ProductMap()
    {
        CreateMap<ProductEntity, ProductResponse>();
        CreateMap<CreateProductRequest, ProductEntity>();
        CreateMap<UpdateProductRequest, ProductEntity>();
    }

    private void UserMap()
    {
        CreateMap<UserEntity, UserResponse>();
        CreateMap<UserEntity, UserSimpleResponse>();
        CreateMap<CreateUserRequest, UserEntity>()
            .ForMember(target => target.PasswordHash, options => options.MapFrom(source => source.Password));
        CreateMap<UpdateUserRequest, UserEntity>()
            .ForMember(target => target.PasswordHash, options => options.MapFrom(source => source.Password));
    }
}