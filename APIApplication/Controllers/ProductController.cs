namespace APIApplication.Controllers;

using EngineApplication.Domain.DTOs.Product;
using EngineApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly IProductRepository _productRepository;

	public ProductsController(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	// ==============================
	// GET: api/products
	// ==============================
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ProductDetailsDto>>> GetAllAsync()
	{
		var products = await _productRepository.GetAllAsync();
		if (products == null || !products.Any())
			return NoContent();

		var result = products.Select(ProductDetailsDto.FromEntity);
		return Ok(result);
	}

	// ==============================
	// GET: api/products/{id}
	// ==============================
	[HttpGet("{id:int}")]
	public async Task<ActionResult<ProductDetailsDto>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid product ID.");

		var product = await _productRepository.GetByIdAsync(id);
		if (product == null)
			return NotFound($"Product with ID {id} not found.");

		return Ok(ProductDetailsDto.FromEntity(product));
	}

	// ==============================
	// POST: api/products
	// ==============================
	[HttpPost]
	public async Task<ActionResult<ProductDetailsDto>> CreateAsync([FromBody] ProductCreateDto dto)
	{
		if (dto == null)
			return BadRequest("Product data is required.");

		var product = dto.ToEntity();
		await _productRepository.AddAsync(product);

		var result = ProductDetailsDto.FromEntity(product);
		return CreatedAtAction(nameof(GetByIdAsync), new { id = result.ProductId }, result);
	}

	// ==============================
	// PUT: api/products/{id}
	// ==============================
	[HttpPut("{id:int}")]
	public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProductUpdateDto dto)
	{
		if (dto == null || id != dto.ProductId)
			return BadRequest("Product ID mismatch.");

		var existing = await _productRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Product with ID {id} not found.");

		existing.Name = dto.Name;
		existing.Description = dto.Description;
		existing.UnitPrice = dto.UnitPrice;
		existing.UpdatedAt = DateTime.UtcNow;

		await _productRepository.UpdateAsync(existing);
		return NoContent();
	}

	// ==============================
	// DELETE: api/products/{id}
	// ==============================
	[HttpDelete("{id:int}")]
	public async Task<ActionResult> DeleteAsync(int id)
	{
		if (id <= 0)
			return BadRequest("Invalid product ID.");

		var existing = await _productRepository.GetByIdAsync(id);
		if (existing == null)
			return NotFound($"Product with ID {id} not found.");

		await _productRepository.DeleteAsync(id);
		return NoContent();
	}
}