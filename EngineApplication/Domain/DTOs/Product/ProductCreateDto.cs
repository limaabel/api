namespace EngineApplication.Domain.DTOs.Product;

public class ProductCreateDto
{
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public decimal UnitPrice { get; set; }

	public Entities.Product ToEntity() => new()
	{
		Name = Name,
		Description = Description,
		UnitPrice = UnitPrice,
		CreatedAt = DateTime.UtcNow,
		UpdatedAt = DateTime.UtcNow
	};
}
