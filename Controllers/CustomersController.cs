using EngineApplication.Domain.Entities;
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

	// GET: api/customers
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Customer>>> GetAllAsync()
	{
		var customers = await _customerRepository.GetAllAsync();
		if (customers == null)
			return NoContent();

		return Ok(customers);
	}

	// GET: api/customers/{id}
	[HttpGet("{id:int}")]
	public async Task<ActionResult<Customer>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid customer ID.");

		var customer = await _customerRepository.GetByIdAsync(id);
		if (customer == null)
			return NotFound($"Customer with ID {id} not found.");

		return Ok(customer);
	}

	[HttpPost]
	public async Task<ActionResult> CreateAsync([FromBody] Customer customer)
	{
		if (customer == null)
			return BadRequest("Customer data is required.");

		await _customerRepository.AddAsync(customer);

		var savedCustomer = await _customerRepository.GetByIdAsync(customer.CustomerId);

		var locationUrl = Url.Action(
			nameof(GetByIdAsync),
			"Customers",
			new { id = savedCustomer.CustomerId },
			Request.Scheme
		);

		return Created(locationUrl!, savedCustomer);
	}

	// PUT: api/customers/{id}
	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateAsync(int id, [FromBody] Customer customer)
	{
		if (customer == null || id != customer.CustomerId)
			return BadRequest("Customer ID mismatch.");

		var existing = await _customerRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Customer with ID {id} not found.");

		await _customerRepository.UpdateAsync(customer);
		return NoContent();
	}

	// DELETE: api/customers/{id}
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