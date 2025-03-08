using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Infra.ORM.NoSQL;

public class OrderDocument
{
    [BsonId]
    public int Id { get; set; }

    [BsonElement("customerId")]
    public int CustomerId { get; set; }

    [BsonElement("customerName")]
    public string CustomerName { get; set; }

    [BsonElement("totalAmount")]
    public decimal TotalAmount { get; set; }

    [BsonElement("orderDate")]
    public DateTime OrderDate { get; set; }

    [BsonElement("status")]
    public string Status { get; set; }

    [BsonElement("items")]
    public List<OrderItemDocument> Items { get; set; }
}