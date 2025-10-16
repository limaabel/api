using EngineApplication.Domain.Entities;
using EngineApplication.Domain.Interfaces;
using InfrastructureApplication.Persistence.Context;

namespace InfrastructureApplication.Persistence.Repositories;

public class SyncLogRepository : ISyncLogRepository
{
	private readonly CrmDbContext _context;

	public SyncLogRepository(CrmDbContext context)
	{
		_context = context;
	}

	public async Task AddAsync(SyncLog log)
	{
		await _context.SyncLogs.AddAsync(log);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<SyncLog>> GetAllAsync()
	{
		return await Task.FromResult(_context.SyncLogs.AsEnumerable());
	}

	public async Task<IEnumerable<SyncLog>> GetByEntityTypeAsync(string entityType)
	{
		return await Task.FromResult(_context.SyncLogs.Where(s => s.EntityType == entityType));
	}

	public async Task<IEnumerable<SyncLog>> GetFailedLogsAsync()
	{
		return await Task.FromResult(_context.SyncLogs.Where(s => s.Status == "Failed"));
	}
}