using Ecommerce.Application.DTOs;
using MediatR;

namespace Ecommerce.Application.Orders.GetOrder;

public class GetOrderByIdQuery : IRequest<OrderDto>
{
    public int Id { get; }

    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}