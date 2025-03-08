using Ecommerce.Domain.Orders;

namespace Ecommerce.Application.Events;

public record OrderCreated(Order Order);