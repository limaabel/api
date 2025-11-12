using EngineApplication.Domain.Entities;

namespace EngineApplication.Domain.Interfaces;

public interface IActivityRepository
{
	Task<IEnumerable<Activity>> GetAllAsync();
	Task<Activity?> GetByIdAsync(int id);
	Task<IEnumerable<Activity>> GetByCustomerIdAsync(int customerId);
	Task AddAsync(Activity activity);
	Task UpdateAsync(Activity activity);
	Task DeleteAsync(int id);
}