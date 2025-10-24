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

	// ==============================
	// GET: api/orders
	// ==============================
	[HttpGet]
	public async Task<ActionResult<IEnumerable<OrderDetailsDto>>> GetAllAsync()
	{
		var orders = await _orderRepository.GetAllAsync();
		if (orders == null || !orders.Any())
			return NoContent();

		var result = orders.Select(OrderDetailsDto.FromEntity);
		return Ok(result);
	}

	// ==============================
	// GET: api/orders/{id}
	// ==============================
	[HttpGet("{id:int}")]
	public async Task<ActionResult<OrderDetailsDto>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid order ID.");

		var order = await _orderRepository.GetByIdAsync(id);
		if (order == null)
			return NotFound($"Order with ID {id} not found.");

		return Ok(OrderDetailsDto.FromEntity(order));
	}

	// ==============================
	// POST: api/orders
	// ==============================
	[HttpPost]
	public async Task<ActionResult<OrderDetailsDto>> CreateAsync([FromBody] OrderCreateDto dto)
	{
		if (dto == null)
			return BadRequest("Order data is required.");

		var order = dto.ToEntity();
		await _orderRepository.AddAsync(order);

		var result = OrderDetailsDto.FromEntity(order);

		return Created(Url.Action(nameof(GetByIdAsync), "Order", new
		{
			id = result.OrderId
		}, Request.Scheme), result);
	}

	// ==============================
	// PUT: api/orders/{id}
	// ==============================
	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateAsync(int id, [FromBody] OrderUpdateDto dto)
	{
		if (dto == null || id != dto.OrderId)
			return BadRequest("Order ID mismatch.");

		var existing = await _orderRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Order with ID {id} not found.");

		existing.Status = dto.Status;
		existing.TotalAmount = dto.TotalAmount;
		existing.CustomerId = dto.CustomerId;
		existing.UpdatedAt = DateTime.UtcNow;

		await _orderRepository.UpdateAsync(existing);
		return NoContent();
	}

	// ==============================
	// DELETE: api/orders/{id}
	// ==============================
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

