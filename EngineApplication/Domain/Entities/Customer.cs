namespace EngineApplication.Domain.Entities;

public class Customer : BaseEntity
{
	public int CustomerId { get; set; }
	public int? ExternalErpId { get; set; }

	public string Name { get; set; } = string.Empty;
	public string? Email { get; set; }
	public string? Phone { get; set; }
	public string? Address { get; set; }
	public string? City { get; set; }
	public string? Country { get; set; }

	// Navigation properties
	public ICollection<Activity> Activities { get; set; } = new List<Activity>();
	public ICollection<Order> Orders { get; set; } = new List<Order>();
	public ICollection<FinancialTransaction> FinancialTransactions { get; set; } = new List<FinancialTransaction>();
}
