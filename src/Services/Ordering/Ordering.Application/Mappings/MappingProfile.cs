using AutoMapper;
using Ordering.Application.Features.Orders;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order,OrdersVm>().ReverseMap();
        CreateMap<Order,CheckoutOrderCommand>().ReverseMap();
    }
}
