namespace EngineApplication.Domain.DTOs.Customer;

public class CustomerDetailsDto
{
	public int CustomerId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Email { get; set; }
	public string? Phone { get; set; }
	public string? Address { get; set; }
	public string? City { get; set; }
	public string? Country { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	public static CustomerDetailsDto FromEntity(Entities.Customer entity) => new()
	{
		CustomerId = entity.CustomerId,
		Name = entity.Name,
		Email = entity.Email,
		Phone = entity.Phone,
		Address = entity.Address,
		City = entity.City,
		Country = entity.Country,
		CreatedAt = entity.CreatedAt,
		UpdatedAt = entity.UpdatedAt
	};
}
