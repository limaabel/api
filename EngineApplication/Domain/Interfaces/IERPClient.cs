using EngineApplication.Domain.Entities;

namespace EngineApplication.Domain.Interfaces;

public interface IERPClient
{
	Task<IEnumerable<Customer>> GetCustomersAsync();
	Task<IEnumerable<Product>> GetProductsAsync();
	Task<IEnumerable<Order>> GetOrdersAsync();
}
