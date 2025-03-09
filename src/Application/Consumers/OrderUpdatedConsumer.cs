using Ecommerce.Application.Events;
using Ecommerce.Domain.Repositories;
using MassTransit;

namespace Ecommerce.Application.Consumers;

public class OrderUpdatedConsumer : IConsumer<OrderUpdated>
{
    private readonly IOrderMongoRepository _orderMongoRepository;

    public OrderUpdatedConsumer(IOrderMongoRepository orderMongoRepository)
    {
        _orderMongoRepository = orderMongoRepository;
    }

    public async Task Consume(ConsumeContext<OrderUpdated> context)
    {
        var order = context.Message.Order;
        await _orderMongoRepository.UpdateOrderAsync(order.Id, order);
    }
}