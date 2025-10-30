namespace EngineApplication.Domain.DTOs.Product;

public class ProductUpdateDto
{
	public int ProductId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public decimal UnitPrice { get; set; }
}
