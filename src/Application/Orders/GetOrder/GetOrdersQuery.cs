using Ecommerce.Application.DTOs;
using MediatR;

namespace Ecommerce.Application.Orders.GetOrder;

public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>> { }