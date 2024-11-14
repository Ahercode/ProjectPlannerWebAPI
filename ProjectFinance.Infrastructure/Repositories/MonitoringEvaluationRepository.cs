using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class MonitoringEvaluationRepository : GenericRepository<MonitoringEvaluation>, IMonitoringEvaluationRepository
{
    public MonitoringEvaluationRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<MonitoringEvaluation>> GetAll()
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(MonitoringEvaluationRepository));
            throw;
        }
    }

    public override Task<MonitoringEvaluation?> GetById(int id)
    {
        try
        {
            return _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetById method error", typeof(MonitoringEvaluationRepository));
            throw;
        }
    }

    public override Task<bool> Update(MonitoringEvaluation entityRequest)
    {
        try
        {
            var existingEntity = _dbSet.FirstOrDefault(x => x.Id == entityRequest.Id);
            if(existingEntity == null)
                return Task.FromResult(false);
            
            
            existingEntity.Id = entityRequest.Id;
            existingEntity.ProjectId = entityRequest.ProjectId;
            existingEntity.Email = entityRequest.Email;
            existingEntity.workDone = entityRequest.workDone;
            existingEntity.Note = entityRequest.Note;
            
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(MonitoringEvaluationRepository));
            throw;
        }
    }

    public override Task<bool> Delete(int id)
    {
        try
        {
            var entity = _dbSet.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return Task.FromResult(false);
            }

            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(MonitoringEvaluationRepository));
            throw;
        }
    }
}