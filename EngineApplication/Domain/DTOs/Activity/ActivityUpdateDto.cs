namespace EngineApplication.Domain.DTOs.Activity;

public class ActivityUpdateDto
{
	public int ActivityId { get; set; }
	public int CustomerId { get; set; }
	public string ActivityType { get; set; } = string.Empty;
	public string Subject { get; set; } = string.Empty;
	public string? Notes { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public string PerformedBy { get; set; } = string.Empty;
}
