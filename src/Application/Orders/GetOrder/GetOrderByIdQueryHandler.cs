using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Repositories;
using MediatR;

namespace Ecommerce.Application.Orders.GetOrder;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderMongoRepository _orderMongoRepository;

    public GetOrderByIdQueryHandler(IOrderMongoRepository orderMongoRepository)
    {
        _orderMongoRepository = orderMongoRepository;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderMongoRepository.GetByIdAsync(request.Id);

        var orderDto = new OrderDto
        {
            Id = order.OrderId,
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            OrderItems = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            }).ToList()
        };

        return orderDto;
    }
}