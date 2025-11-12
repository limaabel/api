using EngineApplication.Domain.Entities;
using EngineApplication.Domain.Interfaces;
using InfrastructureApplication.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureApplication.Persistence.Repositories;

public class ActivityRepository : IActivityRepository
{
	private readonly CrmDbContext _context;

	public ActivityRepository(CrmDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Activity>> GetAllAsync()
	{
		return await _context.Activities
			.Include(a => a.Customer)
			.OrderByDescending(a => a.CreatedAt)
			.ToListAsync();
	}

	public async Task<Activity?> GetByIdAsync(int id)
	{
		return await _context.Activities
			.Include(a => a.Customer)
			.FirstOrDefaultAsync(a => a.ActivityId == id);
	}

	public async Task<IEnumerable<Activity>> GetByCustomerIdAsync(int customerId)
	{
		return await _context.Activities
			.Where(a => a.CustomerId == customerId)
			.OrderByDescending(a => a.CreatedAt)
			.ToListAsync();
	}

	public async Task AddAsync(Activity activity)
	{
		await _context.Activities.AddAsync(activity);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Activity activity)
	{
		_context.Activities.Update(activity);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var existing = await _context.Activities.FindAsync(id);
		if (existing != null)
		{
			_context.Activities.Remove(existing);
			await _context.SaveChangesAsync();
		}
	}
}