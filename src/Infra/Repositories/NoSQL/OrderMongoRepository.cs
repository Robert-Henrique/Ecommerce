using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Repositories;
using Ecommerce.Infra.ORM.NoSQL;
using Ecommerce.Infra.ORM.Settings;
using MongoDB.Driver;

namespace Ecommerce.Infra.ORM.Repositories.NoSQL;

public class OrderMongoRepository : IOrderMongoRepository
{
    private readonly IMongoCollection<OrderDocument> _orders;

    public OrderMongoRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(EnvironmentVariables.GetMongoDatabaseName());
        _orders = database.GetCollection<OrderDocument>("Orders");
    }

    public async Task SaveAsync(OrderReadModel order)
    {
        var orderDocument = new OrderDocument
        {
            Id = order.OrderId,
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,
            TotalAmount = order.TotalAmount,
            OrderDate = order.OrderDate,
            Status = order.Status,
            Items = order.Items.ConvertAll(i => new OrderItemDocument
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            })
        };

        await _orders.InsertOneAsync(orderDocument);

    }

    public async Task<OrderReadModel> GetByIdAsync(int orderId)
    {
        var orderDocument = await _orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();

        if (orderDocument == null) return null;

        return MappingOrderReadModel(orderDocument);
    }

    public async Task<IEnumerable<OrderReadModel>> GetAllAsync()
    {
        var orders = await _orders.Find(FilterDefinition<OrderDocument>.Empty).ToListAsync();

        return orders.Select(MappingOrderReadModel);
    }

    public async Task UpdateOrderAsync(int id, Order updatedOrder)
    {
        var orderDocument = new OrderDocument
        {
            Id = updatedOrder.Id,
            CustomerId = updatedOrder.Customer.Id,
            CustomerName = updatedOrder.Customer.Name,
            TotalAmount = updatedOrder.TotalAmount,
            OrderDate = updatedOrder.OrderDate,
            Status = updatedOrder.Status.ToString(),
            Items = updatedOrder.OrderItems.Select(i => new OrderItemDocument
            {
                ProductId = i.Product.Id,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            }).ToList()
        };

        await _orders.ReplaceOneAsync(o => o.Id == id, orderDocument);
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _orders.DeleteOneAsync(o => o.Id == id);
    }

    private static OrderReadModel MappingOrderReadModel(OrderDocument orderDocument)
    {
        return new OrderReadModel
        {
            OrderId = orderDocument.Id,
            CustomerId = orderDocument.CustomerId,
            CustomerName = orderDocument.CustomerName,
            OrderDate = orderDocument.OrderDate,
            TotalAmount = orderDocument.TotalAmount,
            Status = orderDocument.Status,
            Items = orderDocument.Items.ConvertAll(i => new OrderItemReadModel
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            })
        };
    }
}