namespace EngineApplication.Domain.Entities;

public class FinancialTransaction : BaseEntity
{
	public int TransactionId { get; set; }
	public int? ExternalErpId { get; set; }
	public int CustomerId { get; set; }
	public int? OrderId { get; set; }
	public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
	public string TransactionType { get; set; } = string.Empty; // Invoice, Payment, Refund
	public decimal Amount { get; set; }
	public string Status { get; set; } = "Open";
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	//Relationships
	public Customer Customer { get; set; } = default!;
	public Order? Order { get; set; }
}