using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class POPayScheduleRepository : GenericRepository<POPaySchedule>, IPOPayScheduleRepository
{
    public POPayScheduleRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<POPaySchedule>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(POPayScheduleRepository));
            throw;
        }
    }

    public override async Task<POPaySchedule?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(POPayScheduleRepository));
            throw;
        }
    }

    public override async Task<bool> Update(POPaySchedule poPayScheduleEntity)
    {
        try
        {

            var poPaySchedule = await _dbSet.FirstOrDefaultAsync(x => x.Id == poPayScheduleEntity.Id);
            if (poPaySchedule == null)
                return await Task.FromResult(false);

            poPaySchedule.Id = poPayScheduleEntity.Id;
            poPaySchedule.POId = poPayScheduleEntity.POId;
            poPaySchedule.Amount = poPayScheduleEntity.Amount;;

            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(POPayScheduleRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var poPaySchedule = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (poPaySchedule == null)
                return await Task.FromResult(false);

            _dbSet.Remove(poPaySchedule);
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(POPayScheduleRepository));
            throw;
        }
    }   
}