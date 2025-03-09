using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Infra.ORM.NoSQL;

public class OrderItemDocument
{
    [BsonElement("productId")]
    public int ProductId { get; set; }

    [BsonElement("productName")]
    public string ProductName { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }

    [BsonElement("unitPrice")]
    public decimal UnitPrice { get; set; }

    [BsonElement("totalPrice")]
    public decimal TotalPrice { get; set; }
    
}