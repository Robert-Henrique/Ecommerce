using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Repositories;
using Ecommerce.Domain.ValueObjects;
using MediatR;

namespace Ecommerce.Application.Orders.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, 
        ICustomerRepository customerRepository, 
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
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