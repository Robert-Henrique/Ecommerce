﻿namespace Ecommerce.Domain.Orders;

public class OrderReadModel
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public List<OrderItemReadModel> Items { get; set; }
}