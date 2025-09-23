using Microsoft.AspNetCore.Mvc;

namespace APIApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

	private static readonly List<Product> Products = new()
	{
		new Product { Id = 1, Name = "Product 1", Price = 10.0m },
		new Product { Id = 2, Name = "Product 2", Price = 20.0m },
		new Product { Id = 3, Name = "Product 3", Price = 30.0m }
	};

	[HttpGet]
	public ActionResult<IEnumerable<Product>> GetProducts()
	{
		return Ok(Products);
	}

	[HttpGet("{id}")]
	public ActionResult<Product> GetProduct(int id)
	{
		var product = Products.FirstOrDefault(p => p.Id == id);
		if (product == null)
		{
			return NotFound();
		}
		return Ok(product);
	}

	[HttpPost]
	public ActionResult<Product> CreateProduct(Product product)
	{
		product.Id = Products.Max(p => p.Id) + 1;
		Products.Add(product);
		return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
	}

	[HttpPut("{id}")]
	public IActionResult UpdateProduct(int id, Product updatedProduct)
	{
		var product = Products.FirstOrDefault(p => p.Id == id);
		if (product == null)
		{
			return NotFound();
		}
		product.Name = updatedProduct.Name;
		product.Price = updatedProduct.Price;
		return NoContent();
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteProduct(int id)
	{
		var product = Products.FirstOrDefault(p => p.Id == id);
		if (product == null)
		{
			return NotFound();
		}
		Products.Remove(product);
		return NoContent();
	}


}
