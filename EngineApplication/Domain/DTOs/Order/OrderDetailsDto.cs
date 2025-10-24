namespace EngineApplication.Domain.DTOs.Order;

public class OrderDetailsDto
{
	public int OrderId { get; set; }
	public int CustomerId { get; set; }
	public decimal TotalAmount { get; set; }
	public string? Status { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	public static OrderDetailsDto FromEntity(Entities.Order entity) => new()
	{
		OrderId = entity.OrderId,
		CustomerId = entity.CustomerId,
		TotalAmount = entity.TotalAmount,
		Status = entity.Status,
		CreatedAt = entity.CreatedAt,
		UpdatedAt = entity.UpdatedAt
	};
}