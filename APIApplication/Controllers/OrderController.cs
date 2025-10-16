using EngineApplication.Domain.Entities;
using EngineApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
	private readonly IOrderRepository _orderRepository;

	public OrdersController(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	// GET: api/orders
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
	{
		var orders = await _orderRepository.GetAllAsync();
		if (orders == null)
			return NoContent();

		return Ok(orders);
	}

	// GET: api/orders/{id}
	[HttpGet("{id:int}")]
	public async Task<ActionResult<Order>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid order ID.");

		var order = await _orderRepository.GetByIdAsync(id);
		if (order == null)
			return NotFound($"Order with ID {id} not found.");

		return Ok(order);
	}

	// POST: api/orders
	[HttpPost]
	public async Task<ActionResult> CreateAsync([FromBody] Order order)
	{
		if (order == null)
			return BadRequest("Order data is required.");

		await _orderRepository.AddAsync(order);

		return Created(Url.Action(nameof(GetByIdAsync), "Order", new { id = order.OrderId }, Request.Scheme), order);
	}

	// PUT: api/orders/{id}
	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateAsync(int id, [FromBody] Order order)
	{
		if (order == null || id != order.OrderId)
			return BadRequest("Order ID mismatch.");

		var existing = await _orderRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Order with ID {id} not found.");

		await _orderRepository.UpdateAsync(order);
		return NoContent();
	}

	// DELETE: api/orders/{id}
	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid order ID.");

		var existing = await _orderRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Order with ID {id} not found.");

		await _orderRepository.DeleteAsync(id);
		return NoContent();
	}
}