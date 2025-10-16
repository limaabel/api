namespace EngineApplication.Domain.Entities;

public class Order : BaseEntity
{
	public int OrderId { get; set; }
	public int? ExternalErpId { get; set; }
	public int CustomerId { get; set; }
	public DateTime OrderDate { get; set; } = DateTime.UtcNow;
	public string Status { get; set; } = "Pending";
	public decimal TotalAmount { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

	// Relationships
	public Customer Customer { get; set; } = default!;
	public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	public ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
}