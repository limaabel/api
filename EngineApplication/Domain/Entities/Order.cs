namespace EngineApplication.Domain.Entities;

public class Order : BaseEntity
{
	public int OrderId { get; set; }
	public int? ExternalErpId { get; set; }
	public int CustomerId { get; set; }
	public DateTime OrderDate { get; set; } = DateTime.UtcNow;
	public string Status { get; set; } = "Pending";
	public decimal TotalAmount { get; set; }
	// Relationships
	public Customer Customer { get; set; } = default!;
	public OrderItem[] OrderItems { get; set; } = [];
	public FinancialTransaction[] FinancialTransactions { get; set; } = [];
}