using APIApplication.Model;
using EngineApplication.Domain.Entities;
using EngineApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
	private readonly ICustomerRepository _repo;
	public CustomersController(ICustomerRepository repo) => _repo = repo;

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var all = await _repo.GetAllAsync();
		return Ok(all);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> Get(int id)
	{
		var c = await _repo.GetByIdAsync(id);
		return c == null ? NotFound() : Ok(c);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
	{
		var customer = new Customer
		{
			ExternalErpId = dto.ExternalErpId,
			Name = dto.Name,
			Email = dto.Email,
			Phone = dto.Phone,
			Address = dto.Address,
			City = dto.City,
			Country = dto.Country
		};

		await _repo.AddAsync(customer);
		return CreatedAtAction(nameof(Get), new { id = customer.CustomerId }, customer);
	}
}