namespace EngineApplication.Domain.Entities;

public class Product : BaseEntity
{
	public int ProductId { get; set; }

	public int? ExternalErpId { get; set; }  // optional link to ERP product
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public decimal UnitPrice { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

	// Navigation property — one product can be used in many order items
	public ICollection<OrderItem>? OrderItems { get; set; }
}