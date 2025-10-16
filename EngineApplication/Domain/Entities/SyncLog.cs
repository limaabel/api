namespace EngineApplication.Domain.Entities;

public class SyncLog : BaseEntity
{
	public int SyncId { get; set; }

	public string EntityType { get; set; } = string.Empty; // Customer, Order, Transaction, etc.
	public int EntityId { get; set; }                      // Local CRM entity ID
	public int? ExternalErpId { get; set; }                // ERP reference ID

	public string Operation { get; set; } = string.Empty;  // Import, Export, Update
	public string Status { get; set; } = string.Empty;     // Success, Failed, Skipped
	public string? Message { get; set; }                   // Error or info message

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}