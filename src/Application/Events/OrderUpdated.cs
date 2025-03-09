using Ecommerce.Domain.Orders;

namespace Ecommerce.Application.Events;

public record OrderUpdated(Order Order);