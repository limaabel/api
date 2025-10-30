namespace EngineApplication.Domain.DTOs.Product;

public class ProductDetailsDto
{
	public int ProductId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public decimal UnitPrice { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	public static ProductDetailsDto FromEntity(Entities.Product entity) => new()
	{
		ProductId = entity.ProductId,
		Name = entity.Name,
		Description = entity.Description,
		UnitPrice = entity.UnitPrice,
		CreatedAt = entity.CreatedAt,
		UpdatedAt = entity.UpdatedAt
	};
}
