using Microsoft.Extensions.Logging;
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
    
    public override async Task<MonitoringEvaluation> GetById(int id)
    {
        try
        {
            return await _dbSet
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} GetById method error", typeof(MonitoringEvaluationRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(MonitoringEvaluation monitoringEvaluationEntity)
    {
        try
        {
            var monitoringEvaluation = await _dbSet.FirstOrDefaultAsync(x => x.Id == monitoringEvaluationEntity.Id);
            if (monitoringEvaluation == null)
                return await Task.FromResult(false);

            monitoringEvaluation.Id = monitoringEvaluationEntity.Id;
            monitoringEvaluation.Name = monitoringEvaluationEntity.Name;
            monitoringEvaluation.Description = monitoringEvaluationEntity.Description;
            monitoringEvaluation.StartDate = monitoringEvaluationEntity.StartDate;
            monitoringEvaluation.EndDate = monitoringEvaluationEntity.EndDate;
            
            return true;

        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(MonitoringEvaluationRepository));
            throw;
        }

    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var monitoringEvaluation = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (monitoringEvaluation == null)
                return await Task.FromResult(false);

            _dbSet.Remove(monitoringEvaluation);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(MonitoringEvaluationRepository));
            throw;
        }
    }
}