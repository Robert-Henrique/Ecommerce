using Ecommerce.Application.Events;
using Ecommerce.Domain.Repositories;
using MassTransit;
using MediatR;

namespace Ecommerce.Application.Orders.DeleteOrder;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteOrderHandler(IOrderRepository orderRepository, IPublishEndpoint publishEndpoint)
    {
        _orderRepository = orderRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null) return false;

        await _orderRepository.DeleteAsync(order, cancellationToken);
        await _publishEndpoint.Publish(new OrderDeleted(order.Id), cancellationToken);
        return true;
    }
}