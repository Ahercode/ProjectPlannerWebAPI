
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
{
    public ActivityRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<Activity>> GetAll()
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
            _Logger.LogError(e, "{Repo} AllActivities method error", typeof(ActivityRepository));
            throw;
        }
    }
    

    public override async Task<bool> Update(Activity activityEntity)
    {
        try
        {

            var activity = await _dbSet.FirstOrDefaultAsync(x => x.Id == activityEntity.Id);
            if (activity == null)
                return await Task.FromResult(false);

            activity.Id = activity.Id;
            activity.Name = activity.Name;
            activity.Code = activity.Code;
            
            return true;

        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ActivityRepository));
            throw;
        }

    }
}