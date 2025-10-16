using EngineApplication.Domain.Entities;

namespace EngineApplication.Domain.Interfaces;

public interface ISyncLogRepository
{
	Task AddAsync(SyncLog log);
	Task<IEnumerable<SyncLog>> GetAllAsync();
	Task<IEnumerable<SyncLog>> GetByEntityTypeAsync(string entityType);
	Task<IEnumerable<SyncLog>> GetFailedLogsAsync();
}