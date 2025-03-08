using Ecommerce.Application.DTOs;
using Ecommerce.Application.Orders.CreateOrder;
using Ecommerce.Application.Orders.DeleteOrder;
using Ecommerce.Application.Orders.UpdateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data.");
            }
            
            var command = new CreateOrderCommand(request.CustomerId, request.Items);
            var orderId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderRequestDto request)
        {
            var command = new UpdateOrderCommand(orderId, request.CustomerId, request.Status, request.Items);
            var result = await _mediator.Send(command);

            if (!result) return NotFound();
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var success = await _mediator.Send(new DeleteOrderCommand(id));

            if (!success)
            {
                return NotFound("Order not found.");
            }

            return NoContent();
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            //var order = await _mediator.Send(new GetOrderByIdQuery(id));

            //if (order == null)
            //{
            //    return NotFound();
            //}

            //return Ok(order);

            return Ok();
        }

        //// GET: api/orders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        //{
        //    var orders = await _mediator.Send(new GetOrdersQuery());

        //    return Ok(orders);
        //}
    }
}
