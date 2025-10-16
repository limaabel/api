using EngineApplication.Domain.Entities;
using EngineApplication.Domain.Interfaces;
using InfrastructureApplication.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureApplication.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly CrmDbContext _context;

	public OrderRepository(CrmDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Order>> GetAllAsync()
	{
		return await _context.Orders
			.Include(o => o.OrderItems)
			.Include(o => o.Customer)
			.ToListAsync();
	}

	public async Task<Order> GetByIdAsync(int id)
	{
		return await _context.Orders
			.Include(o => o.OrderItems)
			.Include(o => o.Customer)
			.FirstOrDefaultAsync(o => o.OrderId == id);
	}

	public async Task AddAsync(Order order)
	{
		await _context.Orders.AddAsync(order);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Order order)
	{
		_context.Orders.Update(order);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var order = await GetByIdAsync(id);
		if (order != null)
		{
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
		}
	}
}
