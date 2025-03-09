using Ecommerce.Application.Events;
using Ecommerce.Domain.Repositories;
using MassTransit;

namespace Ecommerce.Application.Consumers;

public class OrderDeletedConsumer : IConsumer<OrderDeleted>
{
    private readonly IOrderMongoRepository _orderMongoRepository;

    public OrderDeletedConsumer(IOrderMongoRepository orderMongoRepository)
    {
        _orderMongoRepository = orderMongoRepository;
    }

    public async Task Consume(ConsumeContext<OrderDeleted> context)
    {
        await _orderMongoRepository.DeleteOrderAsync(context.Message.OrderId);
    }
}