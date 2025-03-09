using Ecommerce.Application.Events;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Repositories;
using MassTransit;

namespace Ecommerce.Application.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    private readonly IOrderMongoRepository _orderMongoRepository;

    public OrderCreatedConsumer(IOrderMongoRepository orderMongoRepository)
    {
        _orderMongoRepository = orderMongoRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var order = context.Message.Order;
        var orderReadModel = new OrderReadModel
        {
            OrderId = order.Id,
            CustomerId = order.Customer.Id,
            CustomerName = order.Customer.Name,
            OrderDate = order.OrderDate,
            Status = order.Status.ToString(),
            TotalAmount = order.TotalAmount,
            Items = order.OrderItems.Select(i => new OrderItemReadModel
            {
                ProductId = i.Product.Id,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            }).ToList()
        };

        await _orderMongoRepository.SaveAsync(orderReadModel);
    }
}