using MediatR;

namespace Ecommerce.Application.Orders.DeleteOrder;

public record DeleteOrderCommand(int OrderId) : IRequest<bool>;