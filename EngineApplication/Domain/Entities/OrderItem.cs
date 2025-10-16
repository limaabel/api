namespace EngineApplication.Domain.Entities;

public class OrderItem : BaseEntity
{
	public int OrderItemId { get; set; }

	public int OrderId { get; set; }
	public Order Order { get; set; }

	public int ProductId { get; set; }          // FK to Product
	public Product Product { get; set; }

	public int Quantity { get; set; } = 1;
	public decimal UnitPrice { get; set; }
	public decimal TotalPrice => Quantity * UnitPrice;
}