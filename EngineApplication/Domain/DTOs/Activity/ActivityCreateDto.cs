namespace EngineApplication.Domain.DTOs.Activity;

public class ActivityCreateDto
{
	public int CustomerId { get; set; }
	public string ActivityType { get; set; } = string.Empty; // e.g., Call, Email, Meeting
	public string Subject { get; set; } = string.Empty;
	public string? Notes { get; set; }
	public string PerformedBy { get; set; } = string.Empty;

	public Entities.Activity ToEntity() => new()
	{
		CustomerId = CustomerId,
		ActivityType = ActivityType,
		Subject = Subject,
		Notes = Notes,
		PerformedBy = PerformedBy,
		CreatedAt = DateTime.UtcNow
	};
}
