using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Repositories;
using MediatR;

namespace Ecommerce.Application.Orders.GetOrder;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderMongoRepository _orderMongoRepository;

    public GetOrdersQueryHandler(IOrderMongoRepository orderMongoRepository)
    {
        _orderMongoRepository = orderMongoRepository;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderMongoRepository.GetAllAsync();
        
        var orderDtos = orders.Select(order => new OrderDto
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
        }).ToList();

        return orderDtos;
    }
}