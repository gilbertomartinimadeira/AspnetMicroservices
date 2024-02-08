using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

        if(orderToUpdate == null){
            _logger.LogError("Order does not exist in database");
            throw new ArgumentException(nameof(orderToUpdate));
        }

        _mapper.Map(request, orderToUpdate,typeof(UpdateOrderCommand),typeof(Order));

        await _orderRepository.UpdateAsync(orderToUpdate);

        _logger.LogInformation("Order successfully updated: "+ orderToUpdate.Id);

        return Unit.Value;
    }
}
