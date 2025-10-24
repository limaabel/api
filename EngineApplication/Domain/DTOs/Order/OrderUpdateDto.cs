namespace EngineApplication.Domain.DTOs.Order;

public class OrderUpdateDto
{
	public int OrderId { get; set; }
	public int CustomerId { get; set; }
	public decimal TotalAmount { get; set; }
	public string? Status { get; set; }
}