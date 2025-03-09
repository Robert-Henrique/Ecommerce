namespace Ecommerce.Application.DTOs;

public record CreateOrderRequestDto(int CustomerId, List<OrderItemRequestDto> Items);