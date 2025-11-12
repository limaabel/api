using EngineApplication.Domain.DTOs.Customer;
using EngineApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
	private readonly ICustomerRepository _customerRepository;

	public CustomersController(ICustomerRepository customerRepository)
	{
		_customerRepository = customerRepository;
	}

	// ==============================
	// GET: api/customers
	// ==============================
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CustomerDetailsDto>>> GetAllAsync()
	{
		var customers = await _customerRepository.GetAllAsync();
		if (customers == null || !customers.Any())
			return NoContent();

		var result = customers.Select(CustomerDetailsDto.FromEntity);
		return Ok(result);
	}

	// ==============================
	// GET: api/customers/{id}
	// ==============================
	[HttpGet("{id:int}", Name = "GetCustomerById")]
	public async Task<ActionResult<CustomerDetailsDto>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid customer ID.");

		var customer = await _customerRepository.GetByIdAsync(id);
		if (customer == null)
			return NotFound($"Customer with ID {id} not found.");

		return Ok(CustomerDetailsDto.FromEntity(customer));
	}

	// ==============================
	// POST: api/customers
	// ==============================
	[HttpPost]
	public async Task<ActionResult<CustomerDetailsDto>> CreateAsync([FromBody] CustomerCreateDto dto)
	{
		if (dto == null)
			return BadRequest("Customer data is required.");

		var customer = dto.ToEntity();
		await _customerRepository.AddAsync(customer);

		var result = CustomerDetailsDto.FromEntity(customer);

		//return Created(Url.Action(nameof(GetByIdAsync), "Customers", new { id = customer.CustomerId }, Request.Scheme), result);
		return CreatedAtRoute("GetCustomerById", new { id = result.CustomerId }, result);
	}

	// ==============================
	// PUT: api/customers/{id}
	// ==============================
	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateAsync(int id, [FromBody] CustomerUpdateDto dto)
	{
		if (dto == null || id != dto.CustomerId)
			return BadRequest("Customer ID mismatch.");

		var existing = await _customerRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Customer with ID {id} not found.");

		// Update fields
		existing.Name = dto.Name;
		existing.Email = dto.Email;
		existing.Phone = dto.Phone;
		existing.Address = dto.Address;
		existing.City = dto.City;
		existing.Country = dto.Country;
		existing.UpdatedAt = DateTime.UtcNow;

		await _customerRepository.UpdateAsync(existing);
		return NoContent();
	}

	// ==============================
	// DELETE: api/customers/{id}
	// ==============================
	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid customer ID.");

		var existing = await _customerRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Customer with ID {id} not found.");

		await _customerRepository.DeleteAsync(id);
		return NoContent();
	}
}
