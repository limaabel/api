namespace EngineApplication.Domain.DTOs.Activity;

public class ActivityDetailsDto
{
	public int ActivityId { get; set; }
	public int CustomerId { get; set; }
	public string ActivityType { get; set; } = string.Empty;
	public string Subject { get; set; } = string.Empty;
	public string? Notes { get; set; }
	public string PerformedBy { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }

	public static ActivityDetailsDto FromEntity(Entities.Activity entity) => new()
	{
		ActivityId = entity.ActivityId,
		CustomerId = entity.CustomerId,
		ActivityType = entity.ActivityType,
		Subject = entity.Subject,
		Notes = entity.Notes,
		PerformedBy = entity.PerformedBy,
		CreatedAt = entity.CreatedAt
	};
}
