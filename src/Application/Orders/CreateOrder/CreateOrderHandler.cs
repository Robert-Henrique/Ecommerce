using Ecommerce.Application.DTOs;
using Ecommerce.Application.Events;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Repositories;
using Ecommerce.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace Ecommerce.Application.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateOrderHandler(ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository, 
        IPublishEndpoint publishEndpoint)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<int> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var custormer = await _customerRepository.GetByIdAsync(command.CustomerId, cancellationToken);

        if (custormer == null)
            throw new KeyNotFoundException($"Custormer with ID {command.CustomerId} not found");

        var orderItems = command.Items.Select(CreateOrderItem);
        var order = new Order(custormer, orderItems);
        var createdOrder = await _orderRepository.CreateAsync(order, cancellationToken);
        await _publishEndpoint.Publish(new OrderCreated(order), cancellationToken);

        return createdOrder.Id;
    }

    private OrderItem CreateOrderItem(OrderItemRequestDto itemRequest)
    {
        var product = _productRepository.GetByIdAsync(itemRequest.ProductId).Result;

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {itemRequest.ProductId} not found");

        var externalIdentity = new ExternalIdentity(product.Id, product.Name);
        return new OrderItem(externalIdentity, itemRequest.Quantity, product.Price);
    }
}