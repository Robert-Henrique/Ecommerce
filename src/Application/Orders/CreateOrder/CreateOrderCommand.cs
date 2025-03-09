using Ecommerce.Application.DTOs;
using MediatR;

namespace Ecommerce.Application.Orders.CreateOrder;

public record CreateOrderCommand(int CustomerId, List<OrderItemRequestDto> Items) : IRequest<int>;