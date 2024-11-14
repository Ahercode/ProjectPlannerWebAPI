using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class FinanceOptionScheduleRepository: GenericRepository<FinanceOptionSchedule>, IFinanceOptionScheduleRepository
{
    public FinanceOptionScheduleRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<FinanceOptionSchedule>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(FinanceOptionScheduleRepository));
            throw;
        }
    }
    
    public override async Task<FinanceOptionSchedule?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(FinanceOptionScheduleRepository));
            throw;
        }
    }

    public override async Task<bool> Update(FinanceOptionSchedule financeOptionScheduleEntity)
    {
        try
        {

            var financeOptionSchedule = await _dbSet.FirstOrDefaultAsync(x => x.Id == financeOptionScheduleEntity.Id);
            if (financeOptionSchedule == null)
                return await Task.FromResult(false);

            financeOptionSchedule.Id = financeOptionScheduleEntity.Id;
            financeOptionSchedule.FinanceOptionId = financeOptionScheduleEntity.FinanceOptionId;

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(FinanceOptionScheduleRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var financeOptionSchedule = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (financeOptionSchedule == null)
                return await Task.FromResult(false);

            _dbSet.Remove(financeOptionSchedule);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(FinanceOptionScheduleRepository));
            throw;
        }
    }
}