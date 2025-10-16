namespace EngineApplication.Domain.Entities;

public class Activity : BaseEntity
{
	public int ActivityId { get; set; }
	public int CustomerId { get; set; }

	public string ActivityType { get; set; } = string.Empty; // Call, Email, Meeting, Note
	public string Subject { get; set; } = string.Empty;
	public string? Notes { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public string PerformedBy { get; set; } = string.Empty;

	// Navigation property
	public Customer Customer { get; set; } = null!;
}
