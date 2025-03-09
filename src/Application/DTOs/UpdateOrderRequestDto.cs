namespace Ecommerce.Application.DTOs;

public record UpdateOrderRequestDto(int CustomerId, int Status, List<OrderItemRequestDto> Items);