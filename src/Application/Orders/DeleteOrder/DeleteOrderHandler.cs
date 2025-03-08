using Ecommerce.Domain.Repositories;
using MediatR;

namespace Ecommerce.Application.Orders.DeleteOrder;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null) return false;

        await _orderRepository.DeleteAsync(order, cancellationToken);
        return true;
    }
}