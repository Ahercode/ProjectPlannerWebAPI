using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectFinance.Domain.Entities;
using ProjectFinance.Infrastructure.DBContext;
using ProjectFinance.Infrastructure.Repositories.Interfaces;

namespace ProjectFinance.Infrastructure.Repositories;

public class CostCategoryRepository : GenericRepository<CostCategory> , ICostCategoryRepository 
{
    public CostCategoryRepository(ProjectFinanceContext context, ILogger logger) : base(context, logger)
    {
    }
    
    public override async Task<IEnumerable<CostCategory>> GetAll()
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
            _Logger.LogError(e, "{Repo} GetAll method error", typeof(CostCategoryRepository));
            throw;
        }
    }
    
    public override async Task<CostCategory?> GetById(int id)
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
            _Logger.LogError(e, "{Repo} GetById method error", typeof(CostCategoryRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(CostCategory costCategoryEntity)
    {
        try
        {
            
            var costCategory = await _dbSet.FirstOrDefaultAsync(x=>x.Id == costCategoryEntity.Id);
            if(costCategory == null)
                return await Task.FromResult(false);
            
            costCategory.Id = costCategoryEntity.Id;
            costCategory.Name = costCategoryEntity.Name;
            costCategory.Code = costCategoryEntity.Code;
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Update method error", typeof(CostCategoryRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(int id)
    {
        try
        {
            var costCategory = await _dbSet.FirstOrDefaultAsync(x=>x.Id == id);
            if(costCategory == null)
                return await Task.FromResult(false);
            
            return true;
        }
        catch (Exception e)
        {
            _Logger.LogError(e, "{Repo} Delete method error", typeof(CostCategoryRepository));
            throw;
        }
    }
}