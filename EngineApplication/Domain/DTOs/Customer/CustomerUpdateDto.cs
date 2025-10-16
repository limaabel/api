namespace EngineApplication.Domain.DTOs.Customer;

public class CustomerUpdateDto
{
	public int CustomerId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Email { get; set; }
	public string? Phone { get; set; }
	public string? Address { get; set; }
	public string? City { get; set; }
	public string? Country { get; set; }
}
