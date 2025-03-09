using Ecommerce.Application.DTOs;
using Ecommerce.Application.Events;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Repositories;
using Ecommerce.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace Ecommerce.Application.Orders.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, 
        ICustomerRepository customerRepository, 
        IProductRepository productRepository, 
        IPublishEndpoint publishEndpoint)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null) return false;

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer == null) return false;

        var orderItems = request.Items.Select(MappingOrderItem)
            .Select(t => t.Result)
            .ToList();

        order.Update(customer, (OrderStatus)request.Status, orderItems);
        await _orderRepository.UpdateAsync(order, cancellationToken);
        await _publishEndpoint.Publish(new OrderUpdated(order), cancellationToken);

        return true;
    }

    private async Task<OrderItem> MappingOrderItem(OrderItemRequestDto orderItemRequest)
    {
        var product = await _productRepository.GetByIdAsync(orderItemRequest.ProductId);
        var quantity = orderItemRequest.Quantity;
        var externalIdentity = new ExternalIdentity(product.Id, product.Name);

        return new OrderItem(externalIdentity, quantity, product.Price);
    }
}