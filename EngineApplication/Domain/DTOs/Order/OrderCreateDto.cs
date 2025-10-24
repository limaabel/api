namespace EngineApplication.Domain.DTOs.Order;

public class OrderCreateDto
{
	public int CustomerId { get; set; }
	public decimal TotalAmount { get; set; }
	public string? Status { get; set; }

	public Entities.Order ToEntity() => new()
	{
		CustomerId = CustomerId,
		TotalAmount = TotalAmount,
		Status = Status ?? "Pending",
		CreatedAt = DateTime.UtcNow,
		UpdatedAt = DateTime.UtcNow
	};
}