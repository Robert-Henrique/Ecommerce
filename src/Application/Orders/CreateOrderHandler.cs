using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Repositories;
using Ecommerce.Domain.ValueObjects;
using MediatR;

namespace Ecommerce.Application.Orders;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandler(ICustomerRepository customerRepository, 
        IProductRepository productRepository, 
        IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var custormer = _customerRepository.GetByIdAsync(command.CustomerId);
        
        if (custormer == null)
            throw new KeyNotFoundException($"Custormer with ID {command.CustomerId} not found");

        var orderItems = command.OrderItems.Select(CreateOrderItem);
        var order = new Order(custormer.Result, orderItems);
        var createdOrder = await _orderRepository.CreateAsync(order, cancellationToken);
        return createdOrder.Id;
    }

    private OrderItem CreateOrderItem(OrderItemCommand command)
    {
        var product = _productRepository.GetByIdAsync(command.ProductId).Result;
        
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {command.ProductId} not found");

        var externalIdentity = new ExternalIdentity(product.Id, product.Name);
        return new OrderItem(externalIdentity, command.Quantity, product.Price);
    }
}