using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class ProjectActivityCostRepository : GenericRepository<ProjectActivityCost>, IProjectActivityCostRepository
{
    public ProjectActivityCostRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<ProjectActivityCost>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(ProjectActivityCostRepository));
            throw;
        }
    }

    public override async Task<ProjectActivityCost?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(ProjectActivityCostRepository));
            throw;
        }
    }

    public override async Task<bool> Update(ProjectActivityCost projectActivityCostEntity)
    {
        try
        {
            var projectActivityCost = await _dbSet.FirstOrDefaultAsync(x => x.Id == projectActivityCostEntity.Id);
            if (projectActivityCost == null)
                return await Task.FromResult(false);

            projectActivityCost.Id = projectActivityCostEntity.Id;
            projectActivityCost.ProjectActivityId = projectActivityCostEntity.ProjectActivityId;
            projectActivityCost.CostDetailId = projectActivityCostEntity.CostDetailId;
            // projectActivityCost.CostCategoryId = projectActivityCostEntity.CostCategoryId;
            // projectActivityCost.CurrencyId = projectActivityCostEntity.CurrencyId;
            projectActivityCost.Amount = projectActivityCostEntity.Amount;

            return true;

        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(ProjectActivityCostRepository));
            throw;
        }
    }
}