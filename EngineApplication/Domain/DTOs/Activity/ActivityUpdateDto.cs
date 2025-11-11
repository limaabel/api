namespace EngineApplication.Domain.DTOs.Activity;

public class ActivityUpdateDto
{
	public int ActivityId { get; set; }
	public string ActivityType { get; set; } = string.Empty;
	public string Subject { get; set; } = string.Empty;
	public string? Notes { get; set; }
	public string PerformedBy { get; set; } = string.Empty;
}
