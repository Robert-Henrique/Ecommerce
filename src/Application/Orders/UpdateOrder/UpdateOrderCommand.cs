using Ecommerce.Application.DTOs;
using MediatR;

namespace Ecommerce.Application.Orders.UpdateOrder;

public record UpdateOrderCommand(int OrderId, int CustomerId, int Status, List<OrderItemRequestDto> Items) : IRequest<bool>;