﻿namespace Ecommerce.Application.DTOs;

public class OrderItemRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}